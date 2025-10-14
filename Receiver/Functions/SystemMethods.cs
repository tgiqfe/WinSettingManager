using Receiver.DataContact;
using WinSettingManager.Functions;
using WinSettingManager.Lib.ADDomain;
using WinSettingManager.Lib.LogonSession;
using WinSettingManager.Lib.OSVersion;
using WinSettingManager.Lib.TuneVolume;
using WinSettingManager.Lib.WindowsService;

namespace Receiver.Lib
{
    public class SystemMethods
    {
        public static Task<string> GetSystemInfo()
        {
            return Task.Run(() =>
            {
                var sessions = UserLogonSession.GetLoggedOnSession();
                string userNames = string.Join(", ", sessions.Select(s => s.UserName).Distinct().Where(n => !string.IsNullOrEmpty(n)));
                return
                    $"OS Version         : {Environment.OSVersion}\n" +
                    $"Machine Name       : {Environment.MachineName}\n" +
                    $"Is Domain PC       : {JoinDomainControl.IsDomainJoined()}\n" +
                    $"Domain Name        : {JoinDomainControl.GetDomainName()}\n" +
                    $"User Name          : {userNames}\n" +
                    $"Processor Count    : {Environment.ProcessorCount}\n" +
                    $"System Directory   : {Environment.SystemDirectory}\n" +
                    $"Current Directory  : {Environment.CurrentDirectory}\n" +
                    $"CLR Version        : {Environment.Version}\n" +
                    $"64-bit OS          : {Environment.Is64BitOperatingSystem}\n" +
                    $"64-bit Process     : {Environment.Is64BitProcess}\n" +
                    $"Logical Drives     : {string.Join(", ", Environment.GetLogicalDrives())}\n" +
                    $"System Uptime (ms) : {Environment.TickCount}\n";
            });
        }



        public static string GetHostName()
        {
            return Environment.MachineName;
        }

        public static string GetDomainName()
        {
            return JoinDomainControl.GetDomainName();
        }

        public static IEnumerable<UserLogonSession> GetLogonSessions()
        {
            return UserLogonSession.GetLoggedOnSession();
        }

        public static string GetOSVersion()
        {
            var os = OSVersions.GetCurrent();
            return $"{os.Name} {os.Edition} [{os.VersionName}]";
        }

        public static DataContactTuneVolume GetSoundVolume()
        {
            var volSummary = VolumeSummary.Load();
            return new DataContactTuneVolume()
            {
                Level = volSummary.Level,
                IsMute = volSummary.IsMute
            };
        }

        public static DataContactTuneVolume SetSoundVolume(DataContactTuneVolume contact)
        {
            if (contact.Level != null) { Sound.SetVolume((float)contact.Level / 100); }
            if (contact.IsMute != null) { Sound.SetMute((bool)contact.IsMute); }

            var volSummary = VolumeSummary.Load();
            return new DataContactTuneVolume()
            {
                Level = volSummary.Level,
                IsMute = volSummary.IsMute
            };
        }

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
