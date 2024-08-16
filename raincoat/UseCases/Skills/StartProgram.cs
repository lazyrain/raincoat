using System.Diagnostics;

namespace raincoat.UseCases.Skills
{
    /// <summary>
    /// プログラムまたはフォルダを開始するためのユースケースを実装します。
    /// </summary>
    public class StartProgram : IUseCase<SkillInputPack, SkillOutputPack>
    {
        // パイプ文字をセパレータとして使用
        private const string Separator = "|";

        /// <summary>
        /// 指定された引数に基づいてプログラムまたはフォルダを開始します。
        /// </summary>
        /// <param name="input">実行に必要な入力パラメータを含む <see cref="SkillInputPack"/> オブジェクト。</param>
        /// <returns>実行結果を含む <see cref="SkillOutputPack"/> オブジェクト。</returns>
        /// <exception cref="ArgumentNullException">入力が null または空の場合にスローされます。</exception>
        /// <exception cref="FileNotFoundException">指定されたパスが存在しない場合にスローされます。</exception>
        public SkillOutputPack Execute(SkillInputPack input)
        {

            if (input == null || string.IsNullOrEmpty(input.Argument))
            {
                throw new ArgumentNullException(nameof(input), "パラメータの設定がおかしい。");
            }

            string[] arguments;
            if (input.Argument.Contains(Separator))
            {
                arguments = input.Argument.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                arguments = new[] { input.Argument };
            }

            foreach (var argumentPath in arguments.Select(arg => arg.Trim()))
            {
                this.ProcessArgument(argumentPath);
            }

            return new SkillOutputPack();
        }

        /// <summary>
        /// 指定されたパスに基づいてプログラムまたはフォルダを開始します。
        /// </summary>
        /// <param name="argumentPath">開始するプログラムまたはフォルダのパス。</param>
        /// <exception cref="FileNotFoundException">指定されたパスが存在しない場合にスローされます。</exception>
        private void ProcessArgument(string argumentPath)
        {
            if (File.Exists(argumentPath))
            {
                Process.Start(argumentPath);
            }
            else if (Directory.Exists(argumentPath))
            {
                Process.Start("explorer.exe", argumentPath);
            }
            else
            {
                throw new FileNotFoundException("指定されたパスが見つかりません。", argumentPath);
            }
        }
    }
}
