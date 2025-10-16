using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace WinSettingManager.Lib.SystemProperties.MemoryDump
{
    /// <summary>
    /// 参考)
    /// https://kogelog.com/2015/09/23/20150923-01/
    /// </summary>
    public class MemoryDumpSetting
    {
        //  参照キー
        //  HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CrashControl

        public DumpType Type { get; set; }
        public string DumpFilePath { get; set; }
        public string MiniDumpDir { get; set; }
        public bool WriteSystemLog { get; set; }
        public bool AutoReboot { get; set; }
        public bool OverwriteExistingFile { get; set; }
        public bool DisableAutomaticDelation { get; set; }

        const string crashControlKey = @"SYSTEM\CurrentControlSet\Control\CrashControl";
        const string name_CRASHDUMPENABLED = "CrashDumpEnabled";
        const string name_FILTERPAGES = "FilterPages";
        const string name_DUMPFILE = "DumpFile";
        const string name_MINIDUMPDIR = "MinidumpDir";
        const string name_LOGEVENT = "LogEvent";
        const string name_AUTOREBOOT = "AutoReboot";
        const string name_OVERWRITE = "Overwrite";
        const string name_ALWAYSKEEPMEMORYDUMP = "AlwaysKeepMemoryDump";

        public MemoryDumpSetting() { }

        /// <summary>
        /// 初期値に設定
        /// </summary>
        public void Init()
        {
            Type = DumpType.Automatic;
            DumpFilePath = @"%SystemRoot%\MEMORY.DMP";
            MiniDumpDir = @"%SystemRoot%\Minidump";
            WriteSystemLog = true;
            AutoReboot = true;
            OverwriteExistingFile = true;
            DisableAutomaticDelation = false;
        }

        /// <summary>
        /// 現在の設定を読み込み
        /// </summary>
        public void Load()
        {
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(crashControlKey, false))
            {
                int dumpParam = (int)regKey.GetValue(name_CRASHDUMPENABLED, 0);
                switch (dumpParam)
                {
                    case 0: Type = DumpType.None; break;
                    case 1:
                        //  CrashDumpEnabledが「1」で、FilterPagesが「1」ならばアクティブメモリダンプ
                        int filterPageParam = (int)regKey.GetValue(name_FILTERPAGES, 0);
                        Type = filterPageParam == 1 ?
                            DumpType.Active :
                            DumpType.Complete;
                        break;
                    case 2: Type = DumpType.Kernel; break;
                    case 3: Type = DumpType.Small; break;
                    case 7: Type = DumpType.Automatic; break;
                }

                DumpFilePath = regKey.GetValue(name_DUMPFILE, "", RegistryValueOptions.DoNotExpandEnvironmentNames) as string;
                MiniDumpDir = regKey.GetValue(name_MINIDUMPDIR, "", RegistryValueOptions.DoNotExpandEnvironmentNames) as string;
                WriteSystemLog = (int)regKey.GetValue(name_LOGEVENT, 1) == 1;
                AutoReboot = (int)regKey.GetValue(name_AUTOREBOOT, 1) == 1;
                OverwriteExistingFile = (int)regKey.GetValue(name_OVERWRITE, 1) == 1;
                DisableAutomaticDelation = (int)regKey.GetValue(name_ALWAYSKEEPMEMORYDUMP, 0) == 1;
            }
        }

        /// <summary>
        /// 設定を更新
        /// </summary>
        public void Save()
        {
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(crashControlKey, true))
            {
                //  ダンプ出力の種類
                int paramValue = 0;
                switch (Type)
                {
                    case DumpType.None: paramValue = 0; break;
                    case DumpType.Complete: paramValue = 1; break;
                    case DumpType.Active: paramValue = 1; break;
                    case DumpType.Kernel: paramValue = 2; break;
                    case DumpType.Small: paramValue = 3; break;
                    case DumpType.Automatic: paramValue = 7; break;
                    default: paramValue = 7; break;
                }
                regKey.SetValue(name_CRASHDUMPENABLED, paramValue, RegistryValueKind.DWord);
                if (Type == DumpType.Active)
                {
                    regKey.SetValue(name_FILTERPAGES, 1, RegistryValueKind.DWord);
                }
                else if (regKey.GetValueNames().Contains(name_FILTERPAGES))
                {
                    regKey.DeleteValue(name_FILTERPAGES);
                }

                //  ダンプ出力先ファイルのパス
                regKey.SetValue(name_DUMPFILE, DumpFilePath, RegistryValueKind.ExpandString);
                string dumpFileDir = Path.GetDirectoryName(DumpFilePath);
                if (!Directory.Exists(dumpFileDir))
                {
                    Directory.CreateDirectory(dumpFileDir);
                }

                //  最小ダンプの出力先フォルダー
                regKey.SetValue(name_MINIDUMPDIR, MiniDumpDir, RegistryValueKind.ExpandString);

                //  システムログにイベントを書き込む
                regKey.SetValue(name_LOGEVENT, WriteSystemLog ? 1 : 0, RegistryValueKind.DWord);

                //  自動的に再起動
                regKey.SetValue(name_AUTOREBOOT, AutoReboot ? 1 : 0, RegistryValueKind.DWord);

                //  既存のファイルに上書き
                regKey.SetValue(name_OVERWRITE, OverwriteExistingFile ? 1 : 0, RegistryValueKind.DWord);

                //  ディスク領域が少ないときでもメモリダンプの自動削除を無効
                //  (自動削除しない)
                regKey.SetValue(name_ALWAYSKEEPMEMORYDUMP, DisableAutomaticDelation ? 1 : 0, RegistryValueKind.DWord);
            }
        }
    }
}
