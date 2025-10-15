using System.Management;
using System.ServiceProcess;

namespace WinSettingManager.Lib.NetworkInfo
{
    public class NetworkData
    {
        public ManagementObject NetworkAdapter { get; set; }
        public ManagementObject NetworkConfig { get; set; }
        public ManagementObject PnpEntity { get; set; }
        public ServiceController DeviceService { get; set; }
    }
}
