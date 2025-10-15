using WinSettingManager.Lib.WindowsService;

namespace Receiver.Functions
{
    public class ServiceMethods
    {
        public static async Task<ServiceSummaryCollection> GetServiceSummaries()
        {
            return await Task.Run(() => ServiceSummaryCollection.Load());
        }

        public static async Task<ServiceSimpleSummaryCollection> GetServiceSimpleSummaries()
        {
            return await Task.Run(() => ServiceSimpleSummaryCollection.Load());
        }

        public static async Task<ServiceSummaryCollection> GetServiceSummary(string name)
        {
            return await Task.Run(() => ServiceSummaryCollection.Load(name));
        }

        public static async Task<ServiceSimpleSummaryCollection> GetServiceSimpleSummary(string name)
        {
            return await Task.Run(() => ServiceSimpleSummaryCollection.Load(name));
        }
    }
}
