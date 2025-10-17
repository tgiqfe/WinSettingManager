using Receiver.DataContact;
using WinSettingManager.Lib.WindowsService;

namespace Receiver.Functions
{
    public class WindowsServiceMethods
    {
        public static async Task<WindowsServiceDataContact> GetServiceSummariesAsync(string name = null)
        {
            return await Task.Run(() =>
            {
                return new WindowsServiceDataContact()
                {
                    ServiceSummaries = ServiceSummary.Load(name).
                        Select(x => new WindowsServiceDataContact.ServiceSummary()
                        {
                            Name = x.Name,
                            DisplayName = x.DisplayName,
                            Status = x.Status.ToString(),
                            StartupType = x.StartupType.ToString(),
                            TriggerStart = x.TriggerStart,
                            DelayedAutoStart = x.DelayedAutoStart,
                            ExecutePath = x.ExecutePath,
                            Description = x.Description,
                            LogonName = x.LogonName,
                            ProcessId = x.ProcessId
                        }).ToArray()
                };
            });
        }

        public static async Task<WindowsServiceDataContact> GetServiceSimpleSummariesAsync(string name = null)
        {
            return new WindowsServiceDataContact()
            {
                ServiceSimpleSummaries = await Task.Run(() =>
                {
                    return ServiceSimpleSummary.Load(name).
                        Select(x => new WindowsServiceDataContact.ServiceSimpleSummary()
                        {
                            Name = x.Name,
                            DisplayName = x.DisplayName,
                            Status = x.Status,
                            StartupType = x.StartupType,
                        }).ToArray();
                })
            };
        }
    }
}
