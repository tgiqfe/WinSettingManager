using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinSettingManager.Lib;

namespace WinSettingManager.Items.Network
{
    public class DHCPClientInfo
    {
        public bool IsDhcpIpEnabled { get; set; }
        public bool IsDhcpDnsEnabled { get; set; }
        public string DhcpServer { get; set; }

        public static DHCPClientInfo Load(ManagementObject netConfig, string deviceID)
        {
            if (string.IsNullOrEmpty(deviceID)) return null;

            DHCPClientInfo ret = null;

            string regKeyPath = @$"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\{deviceID}";
            using (var regKey = RegistryControl.GetRegistryKey(regKeyPath))
            {
                if (regKey != null)
                {
                    ret ??= new();
                    if (netConfig == null)
                    {
                        var tempEnDhcpVal = (int)regKey.GetValue("EnableDHCP", 0);
                        ret.IsDhcpIpEnabled = tempEnDhcpVal != 0;
                        ret.DhcpServer = regKey.GetValue("DhcpServer", "") as string;
                    }
                    else
                    {
                        ret.IsDhcpIpEnabled = (bool)netConfig["DHCPEnabled"];
                        ret.DhcpServer = netConfig["DHCPServer"]?.ToString();
                    }
                    var tempDnsText = regKey.GetValue("NameServer", "") as string;
                    ret.IsDhcpDnsEnabled = ret.IsDhcpIpEnabled && string.IsNullOrEmpty(tempDnsText);
                }
            }

            return ret;
        }
    }
}
