using raincoat.Domains.ValueObjects;
using raincoat.UseCases;
using raincoat.UseCases.Skills;

namespace raincoat.Domains.Services
{
    public static class SkillService
    {
        public static IUseCase<SkillInputPack, SkillOutputPack> Get(SkillType skillType)
        {
            IUseCase<SkillInputPack, SkillOutputPack> result;

            switch (skillType)
            {
                case SkillType.None:
                    result = new Continue();
                    break;
                case SkillType.ChangeScene:
                    result = new ChangeScene();
                    break;
                case SkillType.BeginStream:
                    result = new BeginStream();
                    break;
                case SkillType.EndStream:
                    result = new EndStream();
                    break;
                case SkillType.RunProgram:
                    result = new StartProgram();
                    break;
                default:
                    result = new Continue();
                    break;
            }

            return result;
        }
    }
}
