using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSettingManager.Items.OSVersion;

namespace WinSettingManager.Items.OSVersion.Builder
{
    public class WindowsBuilder
    {
        const int SEED_WINDOWS = 100000;
        const int SEED_WINDOWS_10 = 10;
        const int SEED_WINDOWS_11 = 11;
        const int SEED_WINDOWS_SERVER = 50;

        #region Windows 10

        public static OSVersion Create10ver1507()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.10240",
                VersionAlias = new[] { "1507", "v1507", "10240", "Released in July 2015", "ReleasedinJuly2015", "Threshold 1", "Threshold1", "Release Version", "ReleaseVersion" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 10240,
            };
        }

        public static OSVersion Create10ver1511()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.10586",
                VersionAlias = new[] { "1511", "v1511", "10586", "November Update", "NovemberUpdate", "Threshold 2", "Threshold2" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 10586,
            };
        }

        public static OSVersion Create10ver1607()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.14393",
                VersionAlias = new[] { "1607", "v1607", "14393", "Anniversary Update", "AnniversaryUpdate", "Redstone 1", "Redstone1" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 14393,
            };
        }

        public static OSVersion Create10ver1703()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.15063",
                VersionAlias = new[] { "1703", "v1703", "15063", "Creators Update", "CreatorsUpdate", "Redstone 2", "Redstone2" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 15063,
            };
        }

        public static OSVersion Create10ver1709()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.16299",
                VersionAlias = new[] { "1709", "v1709", "16299", "Fall Creators Update", "FallCreatorsUpdate", "Redstone 3", "Redstone3" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 16299,
            };
        }

        public static OSVersion Create10ver1803()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.17134",
                VersionAlias = new[] { "1803", "v1803", "17134", "April 2018 Update", "April2018Update", "Redstone 4", "Redstone4" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 17134,
            };
        }

        public static OSVersion Create10ver1809()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.17763",
                VersionAlias = new[] { "1809", "v1809", "17763", "October 2018 Update", "October2018Update", "Redstone 5", "Redstone5" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 17763,
            };
        }

        public static OSVersion Create10ver1903()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.18362",
                VersionAlias = new[] { "1903", "v1903", "18362", "May 2019 Update", "May2019Update", "19H1" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 18362,
            };
        }

        public static OSVersion Create10ver1909()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.18636",
                VersionAlias = new[] { "1909", "v1909", "18636", "November 2019 Update", "November2019Update", "19H2" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 18363,
            };
        }

        public static OSVersion Create10ver2004()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.19041",
                VersionAlias = new[] { "2004", "v2004", "19041", "May 2020 Update", "May2020Update", "20H1" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 19041,
            };
        }

        public static OSVersion Create10ver20H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.19042",
                VersionAlias = new[] { "20H2", "v20H2", "19042", "October 2020 Update", "October2020Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 19042,
            };
        }

        public static OSVersion Create10ver21H1()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.19043",
                VersionAlias = new[] { "21H1", "v21H1", "19043", "May 2021 Update", "May2021Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 19043,
            };
        }

        public static OSVersion Create10ver21H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.19044",
                VersionAlias = new[] { "21H2", "v21H2", "19044", "November 2021 Update", "November2021Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 19044,
            };
        }

        public static OSVersion Create10ver22H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 10",
                Alias = new[] { "Windows10", "Windows_10", "Win10" },
                VersionName = "10.0.19045",
                VersionAlias = new[] { "22H2", "v22H2", "19045", "2022 Update", "2022Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_10 + 19045,
            };
        }

        #endregion
        #region Windows 11

        public static OSVersion Create11ver21H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 11",
                Alias = new[] { "Windows11", "Windows_11", "Win11" },
                VersionName = "10.0.22000",
                VersionAlias = new[] { "21H2", "v21H2", "22000", "Sun Valley", "Release Version", "ReleaseVersion" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_11 + 22000,
            };
        }

        public static OSVersion Create11ver22H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 11",
                Alias = new[] { "Windows11", "Windows_11", "Win11" },
                VersionName = "10.0.22621",
                VersionAlias = new[] { "22H2", "v22H2", "22621", "2022 Update", "2022Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_11 + 22621,
            };
        }

        public static OSVersion Create11ver23H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 11",
                Alias = new[] { "Windows11", "Windows_11", "Win11" },
                VersionName = "10.0.22631",
                VersionAlias = new[] { "23H2", "v23H2", "22631", "2023 Update", "2023Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_11 + 22631,
            };
        }

        public static OSVersion Create11ver24H2()
        {
            return new()
            {
                OSFamily = OSFamily.Windows,
                Name = "Windows 11",
                Alias = new[] { "Windows11", "Windows_11", "Win11" },
                VersionName = "10.0.26100",
                VersionAlias = new[] { "24H2", "v24H2", "26100", "2024 Update", "2024Update" },
                Serial = SEED_WINDOWS * SEED_WINDOWS_11 + 26100,
            };
        }

        #endregion
    }
}
