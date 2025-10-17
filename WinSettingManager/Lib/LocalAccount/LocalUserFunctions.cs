using System.DirectoryServices;
using System.Management;

namespace WinSettingManager.Lib.LocalAccount
{
    public class LocalUserFunctions
    {
        /// <summary>
        /// Get Local Users (Computer management parameter)
        /// </summary>
        /// <returns></returns>
        public static LocalUser[] GetLocalUsers()
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
