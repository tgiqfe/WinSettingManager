using Receiver.DataContact;
using WinSettingManager.Lib.WindowsService;

namespace Receiver.Functions
{
    public class WindowsServiceMethods
    {
        /*
        public static async Task<DataContactWindowsService> GetServiceSummaries()
        {
            return await Task.Run(() =>
            {
                return new DataContactWindowsService()
                {
                    ServiceSummaries = ServiceSummary.Load().
                        Select(x => new DataContactWindowsService.ServiceSummary()
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

        public static async Task<DataContactWindowsService> GetServiceSimpleSummaries()
        {
            return await Task.Run(() =>
            {
                return new DataContactWindowsService()
                {
                    ServiceSimpleSummaries = ServiceSimpleSummary.Load().
                        Select(x => new DataContactWindowsService.ServiceSimpleSummary()
                        {
                            Name = x.Name,
                            DisplayName = x.DisplayName,
                            StartupType = x.StartupType,
                        }).ToArray()
                };
            });
        }
        */

        public static async Task<DataContactWindowsService> GetServiceSummaries(string name = null)
        {
            return await Task.Run(() =>
            {
                return new DataContactWindowsService()
                {
                    ServiceSummaries = ServiceSummary.Load(name).
                        Select(x => new DataContactWindowsService.ServiceSummary()
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

        public static async Task<DataContactWindowsService> GetServiceSimpleSummaries(string name = null)
        {
            return new DataContactWindowsService()
            {
                ServiceSimpleSummaries = await Task.Run(() =>
                {
                    return ServiceSimpleSummary.Load(name).
                        Select(x => new DataContactWindowsService.ServiceSimpleSummary()
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
