using Receiver.DataContact;
using WinSettingManager.Lib.SystemProperties;
using WinSettingManager.Lib.LogonSession;
using WinSettingManager.Lib.SoundVolume;
using WinSettingManager.Lib.WindowsService;
using WinSettingManager.Lib.SystemProperties.OSVersion;

namespace Receiver.Functions
{
    public class SystemMethods
    {
        /// <summary>
        /// Get System Information
        /// </summary>
        /// <returns></returns>
        public static async Task<SystemInfoDataContact> GetSystemInfo()
        {
            return await Task.Run(() =>
            {
                var sessions = UserLogonSession.GetLoggedOnSession();
                return new SystemInfoDataContact()
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

        /// <summary>
        /// Get hostname
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// Get domain name or workgroup name
        /// </summary>
        /// <returns></returns>
        public static string GetDomainName()
        {
            return DomainFunctions.GetDomainName();
        }

        /// <summary>
        /// Get OS version
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            var os = OSVersions.GetCurrent();
            return $"{os.Name} {os.Edition} [{os.VersionName}]";
        }
    }
}
