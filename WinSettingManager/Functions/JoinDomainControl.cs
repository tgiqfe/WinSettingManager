using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Functions
{
    public class JoinDomainControl
    {
        [Flags]
        enum JoinOptions
        {
            NONE = 0,
            NETSETUP_JOIN_DOMAIN = 0x00000001,
            NETSETUP_ACCT_CREATE = 0x00000002,
            NETSETUP_ACCT_DELETE = 0x00000004,
            NETSETUP_WIN9X_UPGRADE = 0x00000010,
            NETSETUP_DOMAIN_JOIN_IF_JOINED = 0x00000020,
            NETSETUP_JOIN_UNSECURE = 0x00000040,
            NETSETUP_MACHINE_PWD_PASSED = 0x00000080,
            NETSETUP_DEFERRED_SPN_SET = 0x00000100,
            NETSETUP_JOIN_DC_ACCOUNT = 0x00000200,
            NETSETUP_JOIN_WITH_NEW_NAME = 0x00000400,
            NETSETUP_JOIN_READONLY = 0x00000800,
            NETSETUP_AMBIGUOUS_DC = 0x00001000,
            NETSETUP_NO_NETLOGON_CACHE = 0x00002000,
            NETSETUP_DONT_CONTROL_SERVICES = 0x00004000,
            NETSETUP_SET_MACHINE_NAME = 0x00008000,
            NETSETUP_FORCE_SPN_SET = 0x00010000,
            NETSETUP_NO_ACCT_REUSE = 0x00020000,
            NETSETUP_IGNORE_UNSUPPORTED_FLAGS = 0x10000000,
        }

        private static string _errorMessage(uint retCode)
        {
            return retCode switch
            {
                2 => "The ADOU parameter is not set properly or not working with this current setup",
                5 => "Access Denied",
                87 => "The parameter is incorrect",
                110 => "The system cannot open the specified object",
                1326 => "Logon failure: unknown username or bad password",
                1355 => "The specified domain either does not exist or could not be contacted",
                2103 => "The server could not be located",
                2105 => "A network resource shortage occurred",
                2691 => "The machine is already joined to the domain",
                2692 => "The machine is not currently joined to a domain",
                _ => "Unknown error"
            };
        }

        /// <summary>
        /// Join to Active directory domain.
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="ouPath"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void JoinDomain(string domainName, string userName, string password, string? ouPath = null)
        {
            var managementScope = new ManagementScope(@"\\.\root\cimv2");
            managementScope.Connect();

            using (var computerSystem = new ManagementObject(managementScope, new ManagementPath("Win32_ComputerSystem.Name='" + Environment.MachineName + "'"), null))
            {
                //  Join domain
                var parameters = computerSystem.GetMethodParameters("JoinDomainOrWorkgroup");
                parameters["Name"] = domainName;
                parameters["UserName"] = userName;
                parameters["Password"] = password;
                parameters["FJoinOptions"] = JoinOptions.NETSETUP_JOIN_DOMAIN | JoinOptions.NETSETUP_ACCT_CREATE | JoinOptions.NETSETUP_DOMAIN_JOIN_IF_JOINED;
                parameters["AccountOU"] = ouPath ?? string.Empty;

                var result = computerSystem.InvokeMethod("JoinDomainOrWorkgroup", parameters, null);
                var returnValue = (uint)result["ReturnValue"];

                if (returnValue != 0)
                {
                    string errorMessage = _errorMessage(returnValue);
                    throw new InvalidOperationException($"Fail to join domain, ErrorCode: {returnValue}\r\n{errorMessage}");
                }
            }
        }

        /// <summary>
        /// Unjoin from Active directory domain, and join WORKGROUP.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void UnjoinDomain(string userName, string password)
        {
            var managementScope = new ManagementScope(@"\\.\root\cimv2");
            managementScope.Connect();

            using (var computerSystem = new ManagementObject(managementScope, new ManagementPath("Win32_ComputerSystem.Name='" + Environment.MachineName + "'"), null))
            {
                //  Unjoin domain
                var parameters = computerSystem.GetMethodParameters("UnjoinDomainOrWorkgroup");
                parameters["UserName"] = userName;
                parameters["Password"] = password;
                parameters["FUnjoinOptions"] = JoinOptions.NETSETUP_ACCT_DELETE;

                var result = computerSystem.InvokeMethod("UnjoinDomainOrWorkgroup", parameters, null);
                var returnValue = (uint)result["ReturnValue"];

                if (returnValue != 0)
                {
                    string errorMessage = _errorMessage(returnValue);
                    throw new InvalidOperationException($"Fail to unjoin domain, ErrorCode: {returnValue}\r\n{errorMessage}");
                }

                //  Join workgroup
                var parameters2 = computerSystem.GetMethodParameters("JoinDomainOrWorkgroup");
                parameters2["Name"] = "WORKGROUP";
                parameters2["UserName"] = null;
                parameters2["Password"] = null;
                parameters2["FJoinOptions"] = JoinOptions.NONE;
                parameters2["AccountOU"] = string.Empty;

                var result2 = computerSystem.InvokeMethod("JoinDomainOrWorkgroup", parameters2, null);
                var returnValue2 = (uint)result2["ReturnValue"];

                if (returnValue2 != 0)
                {
                    string errorMessage = _errorMessage(returnValue2);
                    throw new InvalidOperationException($"Fail to join domain, ErrorCode: {returnValue2}\r\n{errorMessage}");
                }
            }
        }


        public static void RenameHostname(string newName, string userName, string password)
        {
            var managementScope = new ManagementScope(@"\\.\root\cimv2");
            managementScope.Connect();

            using (var computerSystem = new ManagementObject(managementScope, new ManagementPath("Win32_ComputerSystem.Name='" + Environment.MachineName + "'"), null))
            {
                //  Rename hostname
                var parameters = computerSystem.GetMethodParameters("Rename");
                parameters["Name"] = newName;
                parameters["UserName"] = userName;
                parameters["Password"] = password;

                var result = computerSystem.InvokeMethod("Rename", parameters, null);
                var returnValue = (uint)result["ReturnValue"];

                if (returnValue != 0)
                {
                    string errorMessage = _errorMessage(returnValue);
                    throw new InvalidOperationException($"Fail to rename, ErrorCode: {returnValue}\r\n{errorMessage}");
                }
            }
        }

        /// <summary>
        /// Is domain computer / workgroup computer
        /// </summary>
        /// <returns></returns>
        public static bool IsDomainJoined()
        {
            var managementScope = new ManagementScope(@"\\.\root\cimv2");
            managementScope.Connect();

            using (var computerSystem = new ManagementObject(managementScope, new ManagementPath("Win32_ComputerSystem.Name='" + Environment.MachineName + "'"), null))
            {
                return computerSystem["PartOfDomain"] as bool? == true;
            }
        }

        /// <summary>
        /// Get joined domain name or workgroup name;
        /// </summary>
        /// <returns></returns>
        public static string GetDomainName()
        {
            var managementScope = new ManagementScope(@"\\.\root\cimv2");
            managementScope.Connect();

            using (var computerSystem = new ManagementObject(managementScope, new ManagementPath("Win32_ComputerSystem.Name='" + Environment.MachineName + "'"), null))
            {
                return computerSystem["Domain"] as string;
            }
        }
    }
}
