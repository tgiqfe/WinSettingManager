using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.ADDomain
{
    public class DomainParam
    {
        public string DomainName { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string OUPath { get; set; }
        public bool? Unjoin { get; set; }
        public bool? Rename { get; set; }
        public string NewComputerName { get; set; }
    }
}
