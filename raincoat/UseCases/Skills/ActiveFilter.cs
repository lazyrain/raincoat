namespace raincoat.UseCases.Skills
{
    internal class ActiveFilter : IUseCase<SkillInputPack, SkillOutputPack>
    {
        public SkillOutputPack Execute(SkillInputPack input)
        {
            input.OBSWebSocketService.ActiveFilter(input.Argument);

            return new SkillOutputPack();
        }
    }
}
