namespace raincoat.UseCases.Skills
{
    public class Continue : IUseCase<SkillInputPack, SkillOutputPack>
    {
        /// <summary>
        /// 何もしない
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SkillOutputPack Execute(SkillInputPack input)
        {
            return new();
        }
    }
}
