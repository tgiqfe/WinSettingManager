using WinSettingManager.Items.LogonSession;
using WinSettingManager.Items.OSVersion;

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

        public static IEnumerable<UserLogonSession> GetLogonSessions()
        {
            return UserLogonSession.GetLoggedOnSession();
        }

        public static string GetOSVersion()
        {
            var os = OSVersions.GetCurrent();
            return $"{os.Name} {os.Edition} [{os.VersionName}]";
        }
    }
}
