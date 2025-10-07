using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Items.Network
{
    internal class NetworkData
    {
        public ManagementObject NetworkAdapter { get; set; }
        public ManagementObject NetworkConfig { get; set; }
        public ManagementObject PnpEntity { get; set; }
        public ServiceController DeviceService { get; set; }
    }
}
