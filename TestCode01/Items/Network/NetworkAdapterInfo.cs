
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;

namespace TestCode01.Items.Network
{
    public class NetworkAdapterInfo
    {
        /// <summary>
        /// Network address information summary.
        /// </summary>
        public string InterfaceName { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        public string MACAddress { get; set; }
        public NetworkAddressSummary NetworkAddressSummary { get; set; }

        public string[] DNSServers { get; set; }
        public bool IsDHCPEnabled { get; set; }
        public string DHCPServer { get; set; }

        public NetworkAdapterInfo(ManagementObject netAdapter, ManagementObject netConfig)
        {
            InterfaceName = netAdapter["Name"]?.ToString();
            DeviceName = netAdapter["InterfaceDescription"]?.ToString();
            DeviceID = netAdapter["DeviceID"]?.ToString();
            MACAddress = netConfig == null ? null : netConfig["MACAddress"]?.ToString();
            NetworkAddressSummary = NetworkAddressSummary.Load(netAdapter, netConfig);
            


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
