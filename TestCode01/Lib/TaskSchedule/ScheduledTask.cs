using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler;

namespace WinSettingManager.Lib.TaskSchedule
{
    public class ScheduledTask
    {
        public TaskGeneral General { get; set; }
        public List<TaskAction> Actions { get; set; }
        public List<TaskTrigger> Triggers { get; set; }
        public TaskRequire Require { get; set; }
        public TaskSetting Setting { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public ScheduledTask()
        {
            this.General = new();
            this.Actions = new();
            this.Triggers = new();
            this.Require = new();
            this.Setting = new();
        }

        public void Regist()
        {
            var taskService = new TaskScheduler.TaskScheduler();
            taskService.Connect(null, null, null, null);
            var rootFolder = taskService.GetFolder(General.TaskPath);
            try
            {
                var definition = taskService.NewTask(0);
                


                //  このへんがマダ


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (rootFolder != null) Marshal.ReleaseComObject(rootFolder);
                if (taskService != null) Marshal.ReleaseComObject(taskService);
            }
        }
    }
}
