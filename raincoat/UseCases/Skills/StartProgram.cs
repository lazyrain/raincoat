using System.Diagnostics;

namespace raincoat.UseCases.Skills
{
    public class StartProgram : IUseCase<SkillInputPack, SkillOutputPack>
    {
        public SkillOutputPack Execute(SkillInputPack input)
        {

            if (input == null || string.IsNullOrEmpty(input.Argument))
            {
                throw new ArgumentNullException(nameof(input), "パラメータの設定がおかしい。");
            }

            var argumentPath = input.Argument;

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

            return new SkillOutputPack();
        }
    }
}
