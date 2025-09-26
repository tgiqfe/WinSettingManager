using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCode01.Items.Network
{
    internal class CheckNetworkAddress
    {
        private static readonly Regex pattern_IPv4Address = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        public static bool IsIPv4Address(string address)
        {
            if (pattern_IPv4Address.IsMatch(address))
            {
                //  192.168.100.0/24
                //  192.168.100.0/255.255.255.0
            }
            else if (address.Contains("*"))
            {
                //  192.168.100.*
                //  192.168.10*
            }
            else if (address.Contains("-") || address.Contains("~"))
            {
                //  192.168.100.1-15
                //  192.168.100.21~30
            }



            return false;
        }


    }
}
