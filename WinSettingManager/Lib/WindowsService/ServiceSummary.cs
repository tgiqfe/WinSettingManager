using System.Management;
using System.ServiceProcess;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSummary : BaseServiceSummary
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ServiceControllerStatus Status { get; set; }
        public ServiceStartMode StartupType { get; set; }
        public bool? TriggerStart { get; set; }
        public bool? DelayedAutoStart { get; set; }
        public string ExecutePath { get; set; }
        public string Description { get; set; }
        public string LogonName { get; set; }
        public long ProcessId { get; set; }

        public ServiceSummary(ServiceController sc, ManagementObject mo = null)
        {
            _sc = sc;
            _mo = mo;

            _mo ??= new ManagementClass("Win32_Service").
                GetInstances().
                OfType<ManagementObject>().
                FirstOrDefault(x => sc.ServiceName == x["Name"] as string);

            this.Name = sc.ServiceName;
            this.DisplayName = sc.DisplayName;
            this.Status = sc.Status;
            this.StartupType = sc.StartType;
            this.TriggerStart = IsTriggeredStart();
            this.TriggerStart = IsDelayedAutoStart();
            if (mo != null)
            {
                this.ExecutePath = mo["PathName"] as string;
                this.Description = mo["Description"] as string;
                this.LogonName = mo["StartName"] as string;
                this.ProcessId = (uint)mo["ProcessId"];
            }
        }
    }
}
