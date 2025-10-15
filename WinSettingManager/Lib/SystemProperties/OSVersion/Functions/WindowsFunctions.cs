using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace WinSettingManager.Lib.SystemProperties.OSVersion.Functions
{
    [SupportedOSPlatform("windows")]
    public class WindowsFunctions
    {
        #region Check ServerOS

        [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsOS(uint os);

        //  OSType
        //  参考
        //  https://www.pinvoke.net/default.aspx/shlwapi.isos

        //private const uint OS_Windows = 0;
        //private const uint OS_NT = 1;
        //private const uint OS_Win95OrGreater = 2;
        //private const uint OS_NT4OrGreater = 3;
        //private const uint OS_Win98OrGreater = 5;
        //private const uint OS_Win98Gold = 6;
        //private const uint OS_Win2000OrGreater = 7;
        //private const uint OS_Win2000Pro = 8;
        //private const uint OS_Win2000Server = 9;
        //private const uint OS_Win2000AdvancedServer = 10;
        //private const uint OS_Win2000DataCenter = 11;
        //private const uint OS_Win2000Terminal = 12;
        //private const uint OS_Embedded = 13;
        //private const uint OS_TerminalClient = 14;
        //private const uint OS_TerminalRemoteAdmin = 15;
        //private const uint OS_Win95Gold = 16;
        //private const uint OS_MEOrGreater = 17;
        //private const uint OS_XPOrGreater = 18;
        //private const uint OS_Home = 19;
        //private const uint OS_Professional = 20;
        //private const uint OS_DataCenter = 21;
        //private const uint OS_AdvancedServer = 22;
        //private const uint OS_Server = 23;
        //private const uint OS_TerminalServer = 24;
        //private const uint OS_PersonalTerminalServer = 25;
        //private const uint OS_FastUserSwitching = 26;
        //private const uint OS_WelcomeLogonUI = 27;
        //private const uint OS_DomainMember = 28;
        private const uint OS_AnyServer = 29;
        //private const uint OS_WOW6432 = 30;
        //private const uint OS_WebServer = 31;
        //private const uint OS_SmallBusinessServer = 32;
        //private const uint OS_TabletPC = 33;
        //private const uint OS_ServerAdminUI = 34;
        //private const uint OS_MediaCenter = 35;
        //private const uint OS_Appliance = 36;

        public static bool IsServer()
        {
            return IsOS(OS_AnyServer);
        }

        #endregion
        #region Get current OS

        /// <summary>
        /// 現在のOSのバージョン情報を取得
        /// </summary>
        /// <returns></returns>
        public static (string, string, string, string, bool) GetCurrent()
        {
            string caption = "";
            string edition = "";
            string version = "";

            try
            {
                //  ManagementClassが使用できる場合
                var mo = new ManagementClass("Win32_OperatingSystem").
                    GetInstances().
                    OfType<ManagementObject>().
                    First();
                caption = mo["Caption"]?.ToString();
                edition = caption.Split(" ").Last();
                version = mo["Version"]?.ToString() ?? "";
            }
            catch
            {
                //  ManagementClassが使用できない場合
                var outTexts = CommandOutput(
                    "powershell", "-Command \"$os = @(Get-CimInstance Win32_OperatingSystem); $os.Caption; $os.Version\"").ToArray();
                caption = outTexts[0];
                edition = caption.Split(" ").Last();
                version = outTexts[1];
            }

            string osName = caption switch
            {
                string s when s.StartsWith("Microsoft Windows 10") => "Windows 10",
                string s when s.StartsWith("Microsoft Windows 11") => "Windows 11",
                string s when s.StartsWith("Microsoft Windows Server") => "Windows Server",
                _ => null,
            };
            bool isServer = IsServer();

            return (osName, caption, edition, version, isServer);
        }

        /// <summary>
        /// コマンド実行結果を取得
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        /// <param name="containsError"></param>
        /// <returns></returns>
        private static IEnumerable<string> CommandOutput(string command, string arguments, bool containsError = false)
        {
            var sb = new StringBuilder();

            using (var proc = new System.Diagnostics.Process())
            {
                proc.StartInfo.FileName = command;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = false;
                proc.OutputDataReceived += (sender, e) => { sb.AppendLine(e.Data); };
                if (containsError) proc.ErrorDataReceived += (sender, e) => { sb.AppendLine(e.Data); };
                proc.Start();
                proc.BeginOutputReadLine();
                if (containsError) proc.BeginErrorReadLine();
                proc.WaitForExit();
            }

            return System.Text.RegularExpressions.Regex.Split(sb.ToString(), @"\r?\n").
                Select(x => x.Trim()).
                Where(x => !string.IsNullOrEmpty(x));
        }

        #endregion
    }
}
