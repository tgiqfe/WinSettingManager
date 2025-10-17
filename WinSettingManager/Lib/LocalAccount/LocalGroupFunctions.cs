using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace WinSettingManager.Lib.LocalAccount
{
    /*
    public class LocalGroupFunctions
    {
        /// <summary>
        /// Get Local Groups (Computer management parameter)
        /// </summary>
        /// <returns></returns>
        public static List<LocalGroup> GetLocalGroups()
        {
            List<LocalGroup> list = new();

            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Group WHERE LocalAccount=True");
            var groups_wmi = searcher.Get();

            using (var directoryEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer"))
            {
                var groups_entry = directoryEntry.Children.
                    OfType<DirectoryEntry>().
                    Where(e => e.SchemaClassName == "Group");

                foreach (var wmi in groups_wmi)
                {
                    var entry = groups_entry.FirstOrDefault(x => string.Equals(x.Name, wmi["Name"]?.ToString(), StringComparison.OrdinalIgnoreCase));
                    if (entry == null) continue;
                    LocalGroup group = new LocalGroup
                    {
                        Name = wmi["Name"]?.ToString(),
                        Description = wmi["Description"]?.ToString(),
                        Members = GetMembersFromDirectoryEntryGroup(entry),
                        SID = wmi["SID"]?.ToString(),
                    };
                    list.Add(group);
                }
            }
            return list;

            string[] GetMembersFromDirectoryEntryGroup(DirectoryEntry groupEntry)
            {
                List<string> list = new();
                var membersEntry = groupEntry.Invoke("Members");
                foreach (var member in (System.Collections.IEnumerable)membersEntry)
                {
                    using (var memberEntry = new DirectoryEntry(member))
                    {
                        list.Add(memberEntry.Name);
                    }
                }
                return list.ToArray();
            }
        }
    }
    */
}
