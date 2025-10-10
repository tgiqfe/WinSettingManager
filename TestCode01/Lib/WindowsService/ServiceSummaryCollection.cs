using System.Management;
using System.ServiceProcess;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSummaryCollection
    {
       public ServiceSummary[] ServiceSummaries { get; set; }

        public static ServiceSummaryCollection Load()
        {
            var services_sc = ServiceController.GetServices();
            var services_mo = new ManagementClass("Win32_Service").
                GetInstances().
                OfType<ManagementObject>();
            return new ServiceSummaryCollection()
            {
                ServiceSummaries = services_sc.
                    Select(sc => new ServiceSummary(sc, services_mo.FirstOrDefault(mo => sc.ServiceName == mo["Name"] as string))).
                    ToArray()
            };
        }
    }
}
