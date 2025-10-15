using System.Management;
using System.ServiceProcess;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSummaryCollection
    {
        public ServiceSummary[] ServiceSummaries { get; set; }

        public static ServiceSummaryCollection Load(string serviceName = null)
        {
            IEnumerable<ServiceController> services = null;
            if (serviceName == null)
            {
                services = ServiceController.GetServices();
            }
            else if (serviceName.Contains("*") || serviceName.Contains("?"))
            {
                var regPattern = TextControl.WildcardMatch(serviceName);
                services = ServiceController.GetServices().
                    Where(x =>
                        regPattern.IsMatch(x.ServiceName) || regPattern.IsMatch(x.DisplayName));
            }
            else
            {
                services = ServiceController.GetServices().
                    Where(x =>
                        x.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase) ||
                        x.DisplayName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            }

            var wmi_services = new ManagementClass("Win32_Service").
                GetInstances().
                OfType<ManagementObject>();
            return new ServiceSummaryCollection()
            {
                ServiceSummaries = services.
                    Select(sc => new ServiceSummary(sc, wmi_services.FirstOrDefault(mo => sc.ServiceName == mo["Name"] as string))).
                    ToArray()
            };
        }
    }
}
