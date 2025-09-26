using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCode01.Items.TaskSchedule
{
    internal class TaskAction
    {
        private readonly Regex pat_arguments = new Regex(@"\s(?=(([^""]*""[^""]*){2})*$)");

        /// <summary>
        /// 実行するプログラム/スクリプトのパス
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 引数
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// 引数を配列で受ける場合
        /// </summary>
        public string[] ArgumentsList
        {
            get
            {
                return string.IsNullOrEmpty(this.Arguments) ?
                    new string[0] { } :
                    pat_arguments.Split(this.Arguments);
            }
            set
            {
                this.ArgumentsList =
                    value.Select(x => x.Contains(" ") ? $"\"{x}\"" : x).ToArray();
            }
        }

        /// <summary>
        /// 作業ディレクトリ
        /// </summary>
        public string WorkingDirectory { get; set; }
    }
}
