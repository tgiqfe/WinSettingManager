using System.ServiceProcess;
using System.Text.RegularExpressions;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSimpleSummaryCollection
    {
        public ServiceSimpleSummary[] ServiceSimpleSummaries { get; set; }

        public static ServiceSimpleSummaryCollection Load(string serviceName = null)
        {
            IEnumerable<ServiceController> services = null;
            if (serviceName == null)
            {
                services = ServiceController.GetServices();
            }
            else if (serviceName.Contains("*") || serviceName.Contains("?"))
            {
                var regPattern = TextFunctions.WildcardMatch(serviceName);
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

            return new ServiceSimpleSummaryCollection()
            {
                ServiceSimpleSummaries = services.Select(x => new ServiceSimpleSummary(x)).ToArray()
            };
        }
    }
}
