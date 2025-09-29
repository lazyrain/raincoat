using raincoat.Domains.Entities;
using raincoat.Domains.ValueObjects;
using raincoat.UseCases;
using raincoat.UseCases.Skills;
using raincoat.UseCases.UseCaseDecorators;

namespace raincoat.Domains.Services
{
    public class SkillService : ISkillService
    {
        public void Execute(SkillType skillType, string argument, ConnectionSetting connectionSetting, OBSWebSocketService obsService)
        {
            var useCase = GetUseCase(skillType);
            var inputPack = new SkillInputPack(obsService, argument, connectionSetting);
            useCase.Execute(inputPack);
        }

        private IUseCase<SkillInputPack, SkillOutputPack> GetUseCase(SkillType skillType)
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
                    result = new ExceptionHandlingDecorator<SkillInputPack, SkillOutputPack>(new StartProgram());
                    break;
                case SkillType.KeyStroke:
                    result = new ReduceKeystrokes();
                    break;
                case SkillType.ActiveFilter:
                    result = new ActiveFilter();
                    break;
                default:
                    result = new Continue();
                    break;
            }

            return result;
        }
    }
}
