using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.LocalAccount
{
    public class LocalUser
    {
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
    }
}
