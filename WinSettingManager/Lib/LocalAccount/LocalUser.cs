using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace WinSettingManager.Lib.LocalAccount
{
    public class LocalUser
    {
        #region public parameter

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public bool UserMustChangePasswordAtNextLogon { get; set; }
        public bool UserCannotChangePassword { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public bool AccountIsDisabled { get; set; }
        public bool AccountIsLockedOut { get; set; }
        public string[] JoinedGroup { get; set; }
        public string ProfilePath { get; set; }
        public string LogonScript { get; set; }
        public string HomeDirectory { get; set; }
        public string HomeDrive { get; set; }
        public string SID { get; set; }

        #endregion

        public static LocalUser[] Load()
        {
            List<LocalUser> list = new();

            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_UserAccount WHERE LocalAccount=True");
            var users_wmi = searcher.Get();

            using (var directoryEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer"))
            {
                var users_entry = directoryEntry.Children.
                    OfType<DirectoryEntry>().
                    Where(e => e.SchemaClassName == "User");

                foreach (var wmi in users_wmi)
                {
                    using (var entry = users_entry.FirstOrDefault(x => string.Equals(x.Name, wmi["Name"]?.ToString(), StringComparison.OrdinalIgnoreCase)))
                    {
                        if (entry == null) continue;
                        LocalUser user = new LocalUser
                        {
                            Name = wmi["Name"]?.ToString(),
                            FullName = wmi["FullName"]?.ToString(),
                            Description = wmi["Description"]?.ToString(),
                            UserMustChangePasswordAtNextLogon = entry.Properties["PasswordExpired"].Value?.ToString() == "1",
                            UserCannotChangePassword = (bool)(wmi["PasswordChangeable"] ?? true) == false,
                            PasswordNeverExpires = (bool)(wmi["PasswordExpires"] ?? true) == false,
                            AccountIsDisabled = (bool)(wmi["Disabled"] ?? false),
                            AccountIsLockedOut = (bool)(wmi["Lockout"] ?? false),
                            JoinedGroup = GetGroupsFromDirectoryEntryUser(entry),
                            ProfilePath = entry.Properties["Profile"].Value?.ToString() ?? string.Empty,
                            LogonScript = entry.Properties["LoginScript"].Value?.ToString() ?? string.Empty,
                            HomeDirectory = entry.Properties["HomeDirectory"].Value?.ToString() ?? string.Empty,
                            HomeDrive = entry.Properties["HomeDirDrive"].Value?.ToString() ?? string.Empty,
                            SID = wmi["SID"]?.ToString(),
                        };
                        list.Add(user);
                    }
                }
            }
            return list.ToArray();

            string[] GetGroupsFromDirectoryEntryUser(DirectoryEntry userEntry)
            {
                List<string> list = new();
                var groupsEntry = userEntry.Invoke("Groups");
                foreach (var group in (System.Collections.IEnumerable)groupsEntry)
                {
                    using (var groupEntry = new DirectoryEntry(group))
                    {
                        list.Add(groupEntry.Name);
                    }
                }
                return list.ToArray();
            }
        }
    }
}
