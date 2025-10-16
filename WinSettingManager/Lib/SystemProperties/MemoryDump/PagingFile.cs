using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.SystemProperties.MemoryDump
{
    public class PagingFile
    {
        private string _driveName = null;
        public string DriveName
        {
            get { return _driveName; }
            set { _driveName = value.ToUpper().TrimEnd('\\'); }
        }
        public PagingFileSizeType Type
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return PagingFileSizeType.None;
                }
                else if (MinimumSize == 0 && MaximumSize == 0)
                {
                    return PagingFileSizeType.SystemManage;
                }
                else
                {
                    return PagingFileSizeType.Custom;
                }
            }
        }
        public string FilePath { get; set; }
        public long MinimumSize { get; set; } = 0;
        public long MaximumSize { get; set; } = 0;

        public PagingFile() { }
    }
}
