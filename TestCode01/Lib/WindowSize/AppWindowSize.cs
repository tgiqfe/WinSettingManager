using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.WindowSize
{
    internal class AppWindowSize
    {
        /// <summary>
        /// Gets the size of the window with the specified title.
        /// </summary>
        /// <param name="appWindowTitle"></param>
        /// <returns></returns>
        public static List<AppWindowSizeSummary> GetWindowSize(string appWindowTitle)
        {
            List<AppWindowSizeSummary> list = new();

            Process[] procs = string.IsNullOrEmpty(appWindowTitle) ?
                Process.GetProcesses() :
                Process.GetProcessesByName(appWindowTitle);
            foreach (Process proc in procs)
            {
                var summary = new AppWindowSizeSummary(proc);
                if (summary.IsWindowProcess)
                {
                    list.Add(summary);
                }
            }

            return list;
        }

        /// <summary>
        /// Change the window size of the specified title.
        /// </summary>
        /// <param name="appWindowTitle"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="withDropShadow"></param>
        public static void SetWindowSize(string appWindowTitle, int left, int top, int width, int height, bool withDropShadow = false)
        {
            var procs = Process.GetProcessesByName(appWindowTitle);
            foreach (var proc in procs)
            {
                var summary = new AppWindowSizeSummary(proc);
                if (withDropShadow)
                {
                    // DropShadowあり
                    summary.ChangeWindowSize(left, top, width, height);
                }
                else
                {
                    // DropShadowなし
                    summary.ChangeWindowSize(
                        left - (summary.X - summary.sX),
                        top - (summary.Y - summary.sY),
                        width + (summary.sWidth - summary.Width),
                        height + (summary.sHeight - summary.Height));
                }
            }
        }
    }
}
