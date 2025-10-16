using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.LocalAccount
{
    internal class LocalUser
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool PasswordMustChange { get; set; }
        public bool PasswordCannotChange { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public bool Enabled { get; set; }
        public bool AccountLocked { get; set; }
        public string[] JoinedGroup { get; set; }
        public string ProfilePath { get; set; }
        public string LogonScript { get; set; }
        public string HomeDirectory { get; set; }
        public string HomeDrive { get; set; }
    }
}
