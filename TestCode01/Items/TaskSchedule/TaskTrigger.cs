using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCode01.Items.TaskSchedule
{
    internal class TaskTrigger
    {
        public enum TriggerType
        {
            None = 0,
            Time = 1,           //  1回のみ実行
            Daily = 2,
            Weekly = 3,
            Monthly = 4,        //  毎月実行(日)
            MonthlyDOW = 5,     //  毎月実行(週)
            Boot = 8,           //  起動時
            Startup = 8,        //  起動時(Bootのエイリアスとして設定)
            Logon = 9,
        }

        /// <summary>
        /// タスクのトリガーの種類
        /// </summary>
        public TriggerType Type { get; set; }

        /// <summary>
        /// 開始日時
        /// </summary>
        public DateTime StartBoundary { get; set; }

        /// <summary>
        /// 間隔(毎日実行)
        /// </summary>
        public short? DaysInterval { get; set; }

        /// <summary>
        /// 間隔(毎週実行)
        /// </summary>
        public short? WeeksInterval { get; set; }

        /// <summary>
        /// 日曜日～土曜日
        /// </summary>
        public short? DaysOfWeek { get; set; }

        /// <summary>
        /// 実行する月
        /// </summary>
        public short? MonthsOfYear { get; set; }

        /// <summary>
        /// 実行する日
        /// </summary>
        public short? DaysOfMonth { get; set; }

        /// <summary>
        /// 実行する週(第何週)
        /// </summary>
        public short? WeeksOfMonth { get; set; }

        /// <summary>
        /// 遅延時間
        /// </summary>
        public TimeSpan? RandomDelay { get; set; }

        /// <summary>
        /// 繰り返し間隔
        /// </summary>
        public TimeSpan? RepetitionInteval { get; set; }

        /// <summary>
        /// 継続時間
        /// </summary>
        public TimeSpan? RepetitionDuration { get; set; }

        /// <summary>
        /// 繰り返し継続時間の最後に実行中のすべてのタスクを停止する
        /// </summary>
        public bool? RepetitionStopAtDurationEnd { get; set; }

        /// <summary>
        /// 停止する前の時間
        /// </summary>
        public TimeSpan? ExecutionTimeLimit { get; set; }

        /// <summary>
        /// 有効期限
        /// </summary>
        public DateTime? EndBoundary { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 有効/無効
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
