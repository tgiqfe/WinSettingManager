using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.LocalAccount
{
    public class LocalGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Members { get; set; }
        public string SID { get; set; }
    }
}
