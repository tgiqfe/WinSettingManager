using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSettingManager.Lib.OSVersion;

namespace WinSettingManager.Lib.OSVersion.Builder
{
    public class AnyOSBuilder
    {
        /// <summary>
        /// Minimum OS
        /// </summary>
        /// <returns></returns>
        public static OSVersion CreateMinimum()
        {
            return new()
            {
                OSFamily = OSFamily.Any,
                Name = "MinimumOS",
                Alias = new string[0] { },
                VersionName = int.MinValue.ToString(),
                VersionAlias = new string[0] { },
                Serial = int.MinValue,
            };
        }

        /// <summary>
        /// Maximum OS
        /// </summary>
        /// <returns></returns>
        public static OSVersion CreateMaximum()
        {
            return new()
            {
                OSFamily = OSFamily.Any,
                Name = "MaximumOS",
                Alias = new string[0] { },
                VersionName = int.MaxValue.ToString(),
                VersionAlias = new string[0] { },
                Serial = int.MaxValue,
            };
        }
    }
}
