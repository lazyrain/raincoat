using raincoat.Domains.Services;

namespace raincoat.UseCases.Skills
{
    public class SkillInputPack : IInputPack
    {
        public SkillInputPack(OBSWebSocketService obsWebSocketService, string argument)
        {
            OBSWebSocketService = obsWebSocketService;
            Argument = argument;
        }

        public OBSWebSocketService OBSWebSocketService { get; private set; }
        public string Argument { get; }
    }
}
