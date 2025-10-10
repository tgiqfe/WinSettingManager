using System.ServiceProcess;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSimpleSummaryCollection
    {
        public ServiceSimpleSummary[] ServiceSimpleSummaries { get; set; }

        public static ServiceSimpleSummaryCollection Load()
        {
            var services = ServiceController.GetServices();
            return new ServiceSimpleSummaryCollection()
            {
                ServiceSimpleSummaries = services.Select(x => new ServiceSimpleSummary(x)).ToArray()
            };
        }
    }
}
