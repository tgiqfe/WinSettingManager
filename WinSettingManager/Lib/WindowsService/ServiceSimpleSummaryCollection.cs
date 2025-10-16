using System.ServiceProcess;
using System.Text.RegularExpressions;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSimpleSummaryCollection
    {
        public ServiceSimpleSummary[] ServiceSimpleSummaries { get; set; }

        /*
        public static ServiceSimpleSummaryCollection Load()
        {
            var services = ServiceController.GetServices();
            return new ServiceSimpleSummaryCollection()
            {
                ServiceSimpleSummaries = services.Select(x => new ServiceSimpleSummary(x)).ToArray()
            };
        }
        */

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

            /*
            Regex willdcardMatch(string text)
            {
                string patternString = Regex.Replace(text, ".",
                    x =>
                    {
                        string y = x.Value;
                        if (y.Equals("?")) { return "."; }
                        else if (y.Equals("*")) { return ".*"; }
                        else { return Regex.Escape(y); }
                    });
                if (!patternString.StartsWith("*")) { patternString = "^" + patternString; }
                if (!patternString.EndsWith("*")) { patternString = patternString + "$"; }
                return new Regex(patternString, RegexOptions.IgnoreCase);
            }
            */
        }
    }
}
