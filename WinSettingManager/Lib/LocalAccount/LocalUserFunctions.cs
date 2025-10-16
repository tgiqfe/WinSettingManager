using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.LocalAccount
{
    internal class LocalUserFunctions
    {
        public static LocalUser[] GetLocalUsers()
        {
            List<LocalUser> localUsers = new();

            
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_UserAccount WHERE LocalAccount=True");
            var results = searcher.Get();
            foreach (var result in results)
            {
                LocalUser user = new LocalUser
                {
                    Name = result["Name"]?.ToString(),
                    Description = result["Description"]?.ToString(),
                    AccountIsDisabled = (bool)(result["Disabled"] ?? false),
                    AccountIsLockedOut = (bool)(result["Lockout"] ?? false),
                };
                localUsers.Add(user);

                
            }

            

            return localUsers.ToArray();
        }
    }
}
