using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.LocalAccount
{
    internal class LocalGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Members { get; set; }
    }
}
