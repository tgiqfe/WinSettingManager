using System.Text.RegularExpressions;
using TestCode01.Lib;

namespace WinSettingManager.Items.Network
{
    public class NetworkAddress
    {
        private readonly Regex pattern_IPv4Address = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        private readonly Regex pattern_IPv6Address = new Regex(@"^(?:(?:(?:[0-9a-f]{1,4}:){7}([0-9a-f]{1,4}|:))|(?:(?:[0-9a-f]{1,4}:){6}(?::[0-9a-f]{1,4}|(?:(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])\.){3}(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])|:))|(?:(?:[0-9a-f]{1,4}:){5}(?:(?:(?::[0-9a-f]{1,4}){1,2})|:(?:(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])\.){3}(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])|:))|(?:(?:[0-9a-f]{1,4}:){4}(?:(?:(?::[0-9a-f]{1,4}){1,3})|(?:(?::[0-9a-f]{1,4})?:(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])(?:\.(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])){3}))|:))|(?:(?:[0-9a-f]{1,4}:){3}(?:(?:(?::[0-9a-f]{1,4}){1,4})|(?:(?::[0-9a-f]{1,4}){0,2}:(?:(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])\.){3}(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9]))|:))|(?:(?:[0-9a-f]{1,4}:){2}(?:(?:(?::[0-9a-f]{1,4}){1,5})|(?:(?::[0-9a-f]{1,4}){0,3}:(?:(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9])\.){3}(?:25[0-5]|(?:2[0-4]|1[0-9]|[1-9]|)[0-9]))|:))|(?:(?:[0-9a-f]{1,4}:){1}(?:(?:(?::[0-9a-f]{1,4}){1,6})|(?:(?::[0-9a-f]{1,4}){0,4}:(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])(?:\.(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])){3}))|:))|(?::(?:(?:(?::[0-9a-f]{1,4}){1,7})|(?:(?::[0-9a-f]{1,4}){0,5}:(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])(?:\.(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])){3}))|:)))(?:%[_a-z0-9]+)?$");

        public enum NetworkAddressType
        {
            IPv4,
            IPv6,
            Unknown,
        }

        public NetworkAddressType AddressType { get; set; }
        public string IPAddress { get; set; }
        public string SubnetMask { get; set; }
        public int? PrefixLength { get; set; }

        public NetworkAddress(string ipAddress, string ipSubnet)
        {
            AddressType = ipAddress switch
            {
                string s when pattern_IPv4Address.IsMatch(s) => NetworkAddressType.IPv4,
                string s when pattern_IPv6Address.IsMatch(s) => NetworkAddressType.IPv6,
                _ => NetworkAddressType.Unknown
            };

            IPAddress = ipAddress;
            if (AddressType == NetworkAddressType.IPv4)
            {
                this.SubnetMask = ipSubnet;
                this.PrefixLength = IPAddressControl.SubnetmaskToInt(ipSubnet);
            }
            else if (AddressType == NetworkAddressType.IPv6)
            {
                this.PrefixLength = int.TryParse(ipSubnet, out int prefixLength) ? prefixLength : null;
            }
        }
    }
}
