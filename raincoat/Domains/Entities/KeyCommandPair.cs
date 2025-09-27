using Newtonsoft.Json;
using raincoat.Domains.ValueObjects;

namespace raincoat.Domains.Entities
{
    public class KeyCommandPair
    {
        [JsonConstructor]
        public KeyCommandPair(string buttonId, string buttonName, SkillType skillType, string argument)
        {
            this.ButtonId = buttonId;
            this.ButtonName = buttonName;
            this.SkillType = skillType;
            this.Argument = argument;
            this.IsWindowTrigger = false;
            this.TriggerWindowTitle = "";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="buttonId">ボタンID</param>
        /// <param name="buttonName">ボタン名</param>
        /// <param name="skillType">コマンド</param>
        /// <param name="argument">コマンドの引数</param>
        /// <param name="isWindowTrigger">アクティブウィンドウによってトリガーするか</param>
        /// <param name="triggerWindowTitle">対象にするアクティブウィンドウ名</param>
        public KeyCommandPair(string buttonId, string buttonName, SkillType skillType, string argument, bool isWindowTrigger, string triggerWindowTitle)
        {
            this.ButtonId = buttonId;
            this.ButtonName = buttonName;
            this.SkillType = skillType;
            this.Argument = argument;
            this.IsWindowTrigger = isWindowTrigger;
            this.TriggerWindowTitle = triggerWindowTitle;
        }

        /// <summary>
        /// ボタンID
        /// </summary>
        public string ButtonId { get; set; }

        /// <summary>
        /// ボタン名
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// コマンド
        /// </summary>
        public SkillType SkillType { get; set; }

        /// <summary>
        /// コマンドの引数
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// ウィンドウ監視をトリガーとするか
        /// </summary>
        public bool IsWindowTrigger { get; set; }

        /// <summary>
        /// 監視対象のウィンドウタイトル
        /// </summary>
        public string TriggerWindowTitle { get; set; }
    }
}
