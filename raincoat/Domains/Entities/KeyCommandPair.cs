using raincoat.Domains.ValueObjects;

namespace raincoat.Domains.Entities
{
    public class KeyCommandPair
    {
        public KeyCommandPair(string buttonId, string buttonName, SkillType skillType, string argument)
        {
            this.ButtonId = buttonId;
            this.ButtonName = buttonName;
            this.SkillType = skillType;
            this.Argument = argument;
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
    }
}
