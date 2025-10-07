using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.TaskSchedule
{
    internal class Functions
    {
        public string ConvertToString(TimeSpan ts)
        {
            var sb = new StringBuilder();
            if (ts.Days > 0)
            {
                sb.Append($"{ts.Days}D");
            }
            if (ts.Hours > 0 || ts.Minutes > 0 || ts.Seconds > 0)
            {
                sb.Append("T");
            }
            if (ts.Hours > 0)
            {
                sb.Append($"{ts.Hours}H");
            }
            if (ts.Minutes > 0)
            {
                sb.Append($"{ts.Minutes}M");
            }
            if (ts.Seconds > 0)
            {
                sb.Append($"{ts.Seconds}S");
            }
            return sb.ToString();
        }

        public static string ToText(TimeSpan? ts)
        {
            return ToText((TimeSpan)ts);
        }

        public static string ToText(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static string ToText(DateTime? dt)
        {
            return ToText((DateTime)dt);
        }
    }
}
