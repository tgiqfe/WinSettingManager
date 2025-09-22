using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TestCode01.Lib;

namespace TestCode01.Items.Network
{
    public class NetworkAddressSummary
    {
        public NetworkAddress[] Addresses { get; set; }
        public string[] DefaultGateway { get; set; }

        public NetworkAddress[] ConfiguredAddresses { get; set; }
        public string[] ConfiguredDefaultGateway { get; set; }

        public NetworkAddressSummary() { }

        public static NetworkAddressSummary Load(ManagementObject netAdapter, ManagementObject netConfig)
        {
            NetworkAddressSummary ret = null;

            // Get current IP addresses from network configuration
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
            }

            // Get configured IP addresses from registry
            string deviceID = netAdapter["DeviceID"]?.ToString();
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
                        ret.ConfiguredAddresses = list.ToArray();
                        ret.ConfiguredDefaultGateway = regKey.GetValue("DefaultGateway") as string[];
                    }
                }
            }

            return ret;
        }
    }
}
