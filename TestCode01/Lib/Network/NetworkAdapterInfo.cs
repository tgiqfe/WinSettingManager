using System.Management;
using System.ServiceProcess;
using WinSettingManager.Lib.Network;

namespace WinSettingManager.Lib.Network
{
    internal class NetworkAdapterInfo
    {
        /// <summary>
        /// Network address information summary.
        /// </summary>
        public string InterfaceName { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public bool? Enabled { get; set; }
        public string MACAddress { get; set; }
        public NetworkAddressSummary NetworkAddress { get; set; }
        public NetworkAddressSummary ConfiguredNetworkAddress { get; set; }
        public DHCPClientInfo DHCPClient { get; set; }

        public NetworkAdapterInfo(NetworkData networkData)
        {
            this.InterfaceName = networkData.NetworkAdapter["Name"]?.ToString();
            this.DeviceName = networkData.NetworkAdapter["InterfaceDescription"]?.ToString();
            this.DeviceID = networkData.NetworkAdapter["DeviceID"]?.ToString();
            this.Enabled = networkData.DeviceService == null ?
                null :
                networkData.DeviceService.Status == ServiceControllerStatus.Running;
            this.MACAddress = networkData.NetworkConfig == null ?
                null :
                networkData.NetworkConfig["MACAddress"]?.ToString();
            this.NetworkAddress = NetworkAddressSummary.LoadFromWMI(networkData.NetworkAdapter, networkData.NetworkConfig);
            this.ConfiguredNetworkAddress = NetworkAddressSummary.LoadFromRegistry(DeviceID);
            this.DHCPClient = NetworkAddress == null && ConfiguredNetworkAddress == null ?
                null :
                DHCPClientInfo.Load(networkData.NetworkConfig, DeviceID);
        }

        public static IEnumerable<NetworkAdapterInfo> Load()
        {
            //  Source data for Network adapter.
            var netAdapters = new ManagementClass(
                    "\\ROOT\\StandardCimv2",
                    "MSFT_NetAdapter",
                    new ObjectGetOptions(null, TimeSpan.MaxValue, true)).
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for Network adapter config.
            var netConfigs = new ManagementClass(
                    "Win32_NetworkAdapterConfiguration").
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for Pnp device.
            var pnpEntities = new ManagementClass(
                    "Win32_PnpEntity").
                GetInstances().
                OfType<ManagementObject>();

            //  Source data for service.
            var devServices = ServiceController.GetDevices();

            var list = new List<NetworkData>();
            foreach (var netAdapter in netAdapters)
            {
                var netConfig = netConfigs.FirstOrDefault(x => x["SettingID"]?.ToString() == netAdapter["DeviceID"]?.ToString());
                var pnpEntity = pnpEntities.FirstOrDefault(x => x["PNPDeviceID"]?.ToString() == netAdapter["PnPDeviceID"]?.ToString());
                var devService = netConfig == null ?
                    null :
                    devServices.FirstOrDefault(x => x.ServiceName == netConfig["ServiceName"]?.ToString().TrimEnd('.'));    //  Some service names contain an unnecessary dot at the end.
                list.Add(new NetworkData()
                {
                    NetworkAdapter = netAdapter,
                    NetworkConfig = netConfig,
                    PnpEntity = pnpEntity,
                    DeviceService = devService
                });
            }
            return list.Select(x => new NetworkAdapterInfo(x));
        }
    }
}
