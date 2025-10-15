using Receiver.DataContact;
using WinSettingManager.Lib.SystemProperties;
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
                    $"Is Domain PC       : {DomainFunctions.IsDomainJoined()}\n" +
                    $"Domain Name        : {DomainFunctions.GetDomainName()}\n" +
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

        public static async Task<DataContactSystemInfo> GetSystemInfoEx()
        {
            return await Task.Run(() =>
            {
                var sessions = UserLogonSession.GetLoggedOnSession();
                return new DataContactSystemInfo()
                {
                    OSVersion = GetOSVersion(),
                    MachineName = Environment.MachineName,
                    IsDomainPC = DomainFunctions.IsDomainJoined(),
                    DomainName = DomainFunctions.GetDomainName(),
                    LoggedOnUsers = sessions.Select(s => s.UserName).Distinct().Where(n => !string.IsNullOrEmpty(n)).ToArray(),
                    ProcessorCount = Environment.ProcessorCount,
                    SystemMemoryMB = (int)(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / (1024 * 1024)),
                    Is64BitOS = Environment.Is64BitOperatingSystem,
                    LogicalDrives = Environment.GetLogicalDrives(),
                    MachineSerial = ComputerFunctions.GetMachineSerial()
                };
            });
        }


        public static string GetHostName()
        {
            return Environment.MachineName;
        }

        public static string GetDomainName()
        {
            return DomainFunctions.GetDomainName();
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
