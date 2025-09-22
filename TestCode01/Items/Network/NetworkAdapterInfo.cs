
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;

namespace TestCode01.Items.Network
{
    internal class NetworkAdapterInfo
    {
        /// <summary>
        /// Network address information summary.
        /// </summary>
        public string InterfaceName { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public string MACAddress { get; set; }
        public NetworkAddressSummary NetworkAddress { get; set; }
        public NetworkAddressSummary ConfiguredNetworkAddress { get; set; }
        public DHCPClientInfo DHCPClient { get; set; }

        public NetworkAdapterInfo(ManagementObject netAdapter, ManagementObject netConfig)
        {
            this.InterfaceName = netAdapter["Name"]?.ToString();
            this.DeviceName = netAdapter["InterfaceDescription"]?.ToString();
            this.DeviceID = netAdapter["DeviceID"]?.ToString();
            this.MACAddress = netConfig == null ? null : netConfig["MACAddress"]?.ToString();
            this.NetworkAddress = NetworkAddressSummary.LoadFromWMI(netAdapter, netConfig);
            this.ConfiguredNetworkAddress = NetworkAddressSummary.LoadFromRegistry(DeviceID);
            this.DHCPClient = DHCPClientInfo.Load(netConfig, DeviceID, NetworkAddress == null && ConfiguredNetworkAddress == null);

        }


        public static IEnumerable<NetworkAdapterInfo> Load()
        {
            var netConfigs = new ManagementClass(
                    "Win32_NetworkAdapterConfiguration").
                GetInstances().
                OfType<ManagementObject>();
            var netAdapters = new ManagementClass(
                    "\\ROOT\\StandardCimv2",
                    "MSFT_NetAdapter",
                    new ObjectGetOptions(null, TimeSpan.MaxValue, true)).
                GetInstances().
                OfType<ManagementObject>();

            return netAdapters.
                ToList().
                Select(x => new NetworkAdapterInfo(x, netConfigs.FirstOrDefault(y => y["SettingID"]?.ToString() == x["DeviceID"]?.ToString())));
        }
    }
}
