using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace raincoat.Domains.ValueObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillType
    {
        /// <summary>
        /// なし
        /// </summary>
        None = 0,
        /// <summary>
        /// シーン切り替え
        /// </summary>
        ChangeScene = 1,
        /// <summary>
        /// 配信開始
        /// </summary>
        BeginStream = 2,
        /// <summary>
        /// 配信終了
        /// </summary>
        EndStream = 3,
        /// <summary>
        /// パス起動
        /// </summary>
        RunProgram = 4,
        /// <summary>
        /// キーを押す
        /// </summary>
        KeyStroke = 5,
        /// <summary>
        /// フィルターオン
        /// </summary>
        ActiveFilter = 6,
    }
}