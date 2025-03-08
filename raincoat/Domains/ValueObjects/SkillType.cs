namespace raincoat.Domains.ValueObjects
{
    public enum SkillType
    {
        /// <summary>
        /// 設定なし
        /// </summary>
        None = 0,
        /// <summary>
        /// シーン変更
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
        /// プログラム実行
        /// </summary>
        RunProgram = 4,
        /// <summary>
        /// キー押下
        /// </summary>
        KeyStroke = 5,
        /// <summary>
        /// フィルターアクティブ
        /// </summary>
        ActiveFilter = 6,
    }
}