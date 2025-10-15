using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.SystemProperties
{
    public class ComputerFunctions
    {
        public static string GetMachineSerial()
        {
            var mo = new ManagementClass("Win32_BIOS").
                GetInstances().
                OfType<ManagementObject>().
                First();
            return mo["SerialNumber"]?.ToString() ?? string.Empty;
        }
    }
}
