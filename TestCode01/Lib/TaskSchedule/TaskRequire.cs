using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSettingManager.Lib.TaskSchedule
{
    public class TaskRequire
    {
        /// <summary>
        /// 次の間アイドル状態の場合のみタスクを開始する
        /// </summary>
        public bool? RunOnlyIfIdle { get; set; }

        /// <summary>
        /// タスクを開始するまでのコンピュータのアイドル時間
        /// </summary>
        public TimeSpan? IdleDuration { get; set; }

        /// <summary>
        /// アイドル状態になるのを待機する時間
        /// </summary>
        public TimeSpan? WaitTimeout { get; set; }

        /// <summary>
        /// コンピュータがアイドル状態でなくなった場合は停止する
        /// </summary>
        public bool? StopOnIdleEnd { get; set; }

        /// <summary>
        /// 再びアイドル状態になったら再開する
        /// </summary>
        public bool? RestartOnIdle { get; set; }

        /// <summary>
        /// コンピューターをAC電源で使用している場合のみタスクを開始する
        /// </summary>
        public bool? DisallowStartIfOnBatteries { get; set; }

        /// <summary>
        /// コンピューターの電源をバッテリに切り替える場合は停止する
        /// </summary>
        public bool? StopIfGoingOnBatteries { get; set; }

        /// <summary>
        /// タスクを実行するためにスリープを解除する
        /// </summary>
        public bool? WakeToRun { get; set; }

        /// <summary>
        /// 次のネットワーク接続が使用可能な場合のみタスクを開始する
        /// </summary>
        public bool? RunOnlyIfNetworkAvailable { get; set; }

        /// <summary>
        /// 使用可能な場合に実行するネットワークの名前
        /// </summary>
        public string NetworkName { get; set; }
    }
}
