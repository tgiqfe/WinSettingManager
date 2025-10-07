using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WinSettingManager.Lib;

namespace WinSettingManager.Functions
{
    public class ServiceControl
    {
        /// <summary>
        /// Get ServiceController instance from service name or display name.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static ServiceController GetServiceController(string serviceName)
        {
            if (serviceName.Contains("*") || serviceName.Contains("?"))
            {
                var regPattern = willdcardMatch(serviceName);
                return ServiceController.GetServices().
                    FirstOrDefault(x =>
                        regPattern.IsMatch(x.ServiceName) || regPattern.IsMatch(x.DisplayName));
            }
            else
            {
                return ServiceController.GetServices().
                    FirstOrDefault(x =>
                        x.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase) ||
                        x.DisplayName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            }

            Regex willdcardMatch(string text)
            {
                string patternString = Regex.Replace(text, ".",
                    x =>
                    {
                        string y = x.Value;
                        if (y.Equals("?")) { return "."; }
                        else if (y.Equals("*")) { return ".*"; }
                        else { return Regex.Escape(y); }
                    });
                if (!patternString.StartsWith("*")) { patternString = "^" + patternString; }
                if (!patternString.EndsWith("*")) { patternString = patternString + "$"; }
                return new Regex(patternString, RegexOptions.IgnoreCase);
            }
        }

        /// <summary>
        /// Is the service a delayed auto start service?
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="mo"></param>
        /// <returns></returns>
        public static bool IsDelayedAutoStart(ServiceController sc, ManagementObject mo = null)
        {
            if (sc == null) return false;
            if (mo == null)
            {
                var keyPath = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services";
                using (var regKey = RegistryControl.GetRegistryKey(keyPath, false))
                {
                    if (regKey != null)
                    {
                        using (var subKey = regKey.OpenSubKey(sc.ServiceName))
                        {
                            if (subKey != null)
                            {
                                var startValue = subKey.GetValue("Start");
                                var delayedAutoStartValue = subKey.GetValue("DelayedAutostart");
                                if (startValue != null && delayedAutoStartValue != null)
                                {
                                    return (int)startValue == 2 && (int)delayedAutoStartValue == 1;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return sc.StartType == ServiceStartMode.Automatic && (bool)mo["DelayedAutoStart"];
            }
            return false;
        }

        /// <summary>
        /// Is the service a trigger start service?
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public static bool IsTriggeredStart(ServiceController sc)
        {
            if (sc == null) return false;
            var keyPath = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services";
            using (var regKey = RegistryControl.GetRegistryKey(keyPath, false))
            {
                if (regKey != null)
                {
                    using (var subKey = regKey.OpenSubKey(sc.ServiceName))
                    {
                        if (subKey != null)
                        {
                            return subKey.GetSubKeyNames().Any(x =>
                                x.Equals("TriggerInfo", StringComparison.OrdinalIgnoreCase));
                        }
                    }
                }
            }
            return false;
        }
    }
}
