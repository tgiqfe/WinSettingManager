using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TestCode01.Lib;

namespace TestCode01.Items.Network
{
    internal class NetworkAddressSummary
    {
        public NetworkAddress[] Addresses { get; set; }
        public string[] DefaultGateway { get; set; }
        public string[] DNSServers { get; set; }

        /// <summary>
        /// Get current IP addresses from network configuration
        /// </summary>
        /// <param name="netAdapter"></param>
        /// <param name="netConfig"></param>
        /// <returns></returns>
        public static NetworkAddressSummary LoadFromWMI(ManagementObject netAdapter, ManagementObject netConfig)
        {
            if (netAdapter == null || netConfig == null) return null;

            NetworkAddressSummary ret = null;
            if (netConfig != null && netConfig["IPAddress"] != null)
            {
                List<NetworkAddress> list = new();
                var addresses = netConfig["IPAddress"] as string[];
                var subnets = netConfig["IPSubnet"] as string[];
                for (int i = 0; i < addresses.Length; i++)
                {
                    string ipAddress = addresses[i];
                    string ipSubnet = subnets != null && i < subnets.Length ? subnets[i] : string.Empty;
                    list.Add(new NetworkAddress(ipAddress, ipSubnet));
                }
                ret = new();
                ret.Addresses = list.ToArray();
                ret.DefaultGateway = netConfig["DefaultIPGateway"] as string[];
                ret.DNSServers = netConfig["DNSServerSearchOrder"] as string[];

                if (IsEmptyParameter(ret.DefaultGateway)) ret.DefaultGateway = null;
                if (IsEmptyParameter(ret.DNSServers)) ret.DNSServers = null;
            }

            return ret;
        }

        /// <summary>
        /// Get configured IP addresses from registry
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static NetworkAddressSummary LoadFromRegistry(string deviceID)
        {
            if (string.IsNullOrEmpty(deviceID)) return null;

            NetworkAddressSummary ret = null;
            string regKeyPath = @$"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\{deviceID}";
            using (var regKey = RegistryControl.GetRegistryKey(regKeyPath))
            {
                if (regKey != null)
                {
                    var addresses = regKey.GetValue("IPAddress") as string[];
                    var subnets = regKey.GetValue("SubnetMask") as string[];
                    if (addresses?.Length > 0)
                    {
                        List<NetworkAddress> list = new();
                        for (int i = 0; i < addresses.Length; i++)
                        {
                            string ipAddress = addresses[i];
                            string ipSubnet = subnets != null && i < subnets.Length ? subnets[i] : string.Empty;
                            list.Add(new NetworkAddress(ipAddress, ipSubnet));
                        }
                        ret ??= new();
                        ret.Addresses = list.ToArray();
                        ret.DefaultGateway = regKey.GetValue("DefaultGateway") as string[];
                        ret.DNSServers = regKey.GetValue("NameServer")?.ToString().Split(',').Select(x => x.Trim()).ToArray();

                        if (IsEmptyParameter(ret.DefaultGateway)) ret.DefaultGateway = null;
                        if (IsEmptyParameter(ret.DNSServers)) ret.DNSServers = null;
                    }
                }
            }

            return ret;
        }

        private static bool IsEmptyParameter(string[] array)
        {
            return array == null ||
                array.Length == 0 ||
                array.All(x => string.IsNullOrEmpty(x));
        }
    }
}
