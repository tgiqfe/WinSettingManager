using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler;

namespace WinSettingManager.Items.TaskSchedule
{
    internal class TaskGeneral
    {
        /// <summary>
        /// タスクの名前
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// タスクの場所(タスクのパス,フォルダー)
        /// </summary>
        public string TaskPath { get; set; } = "\\";

        /// <summary>
        /// タスクの製作者
        /// </summary>
        public string Auther { get; set; }

        /// <summary>
        /// タスクの説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// タスク実行時に使うユーザーアカウント
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ユーザーがログオンしているときのみ実行する
        /// </summary>
        public bool? RunOnlyWhenUserLoggedOn { get; set; }

        /// <summary>
        /// パスワードを保存しない
        /// </summary>
        public bool? DoNotSavePassword { get; set; }

        /// <summary>
        /// 最上位の特権で実行する
        /// </summary>
        public bool? RunWithHighest { get; set; }

        /// <summary>
        /// 表示しない
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// タスク実行時のログオンタイプ
        /// - TASK_LOGON_INTERACTIVE_TOKEN
        ///   ⇒ユーザーがログオンしているときのみ実行する
        /// - TASK_LOGON_S4U
        ///   ⇒ユーザーがログオンしているかどうかに関わらず実行する & パスワードを保存しない
        /// - TASK_LOGON_PASSWORD
        ///   ⇒ユーザーがログオンしているかどうかに関わらず実行する & パスワードを保存する
        /// </summary>
        public _TASK_LOGON_TYPE LogonType
        {
            get
            {
                if (RunOnlyWhenUserLoggedOn ?? true)
                {
                    return _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN;
                }
                if (DoNotSavePassword ?? false)
                {
                    return _TASK_LOGON_TYPE.TASK_LOGON_S4U;
                }
                return _TASK_LOGON_TYPE.TASK_LOGON_PASSWORD;
            }
        }

    }
}
