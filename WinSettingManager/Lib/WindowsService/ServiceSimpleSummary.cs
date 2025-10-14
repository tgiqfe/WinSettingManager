using System.ServiceProcess;
using WinSettingManager.Functions;

namespace WinSettingManager.Lib.WindowsService
{
    public class ServiceSimpleSummary : BaseServiceSummary
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string StartupType { get; set; }

        public ServiceSimpleSummary(ServiceController sc)
        {
            this.Name = sc.ServiceName;
            this.DisplayName = sc.DisplayName;
            this.StartupType = sc.StartType switch
            {
                ServiceStartMode.Automatic => "自動",
                ServiceStartMode.Manual => "手動",
                ServiceStartMode.Disabled => "無効",
                _ => "不明"
            };

            var delay = IsDelayedAutoStart(sc);
            var trigger = IsTriggeredStart(sc);
            if (delay || trigger)
            {
                List<string> list = new();
                if (delay) list.Add("遅延自動");
                if (trigger) list.Add("トリガー開始");
                this.StartupType += " (" + string.Join(",", list) + ")";
            }
        }
    }
}
