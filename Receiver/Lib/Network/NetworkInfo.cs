
namespace WinSettingManager.Lib.Network
{
    public class NetworkInfo
    {
        /// <summary>
        /// IPv4 Address information
        /// </summary>
        public class NetworkIPv4Address
        {
            public string IPAddress { get; set; }
            public string SubnetMask { get; set; }
        }

        /// <summary>
        /// IPv6 Address information
        /// </summary>
        public class NetworkIPv6Address
        {
            public string IPAddress { get; set; }
            public int PrefixLength { get; set; }
        }

        /// <summary>
        /// Network address information summary.
        /// </summary>
        public string InterfaceName { get; set; }
        public string DeviceName { get; set; }
        public NetworkIPv4Address[] IPv4Address { get; set; }
        public string IPv4DefaultGateway { get; set; }
        public NetworkIPv6Address[] IPv6Address { get; set; }
        public string IPv6DefaultGateway { get; set; }
        public string[] DNSServers { get; set; }
        public string MACAddress { get; set; }
        public bool IsDHCPEnabled { get; set; }
        public string DHCPServer { get; set; }

        
    }
}
