using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    internal class ServiceSummary
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
        public int? ProcessId { get; set; }
        public string[] ServicesDependedOn { get; set; }
        public string[] DependentServices { get; set; }

        public ServiceSummary() { }
        public ServiceSummary(ServiceController sc)
        {
            if (sc != null)
            {
                ManagementObject mo = new ManagementClass("Win32_Service").
                    GetInstances().
                    OfType<ManagementObject>().
                    FirstOrDefault(x =>
                        x["Name"] != null &&
                        (x["Name"] as string).Equals(sc.ServiceName, StringComparison.OrdinalIgnoreCase));

                this.Name = sc.ServiceName;
                this.DisplayName = sc.DisplayName;
                this.Status = sc.Status;
                this.StartupType = sc.StartType;
                this.TriggerStart = ServiceControl.IsTriggeredStart(sc);
                this.TriggerStart = ServiceControl.IsDelayedAutoStart(sc, mo);
                this.ExecutePath = mo["PathName"] as string;
                this.Description = mo["Description"] as string;
                this.LogonName = mo["StartName"] as string;
                this.ProcessId = mo["ProcessId"] as int?;
                this.ServicesDependedOn = sc.ServicesDependedOn.Select(x => x.ServiceName).ToArray();
                this.DependentServices = sc.DependentServices.Select(x => x.ServiceName).ToArray();
            }
        }
    }
}
