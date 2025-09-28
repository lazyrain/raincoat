using raincoat.Domains.Entities;
using raincoat.Domains.Services;

namespace raincoat.UseCases.Skills
{
    public class SkillInputPack : IInputPack
    {
        public SkillInputPack(OBSWebSocketService obsWebSocketService, string argument, ConnectionSetting? connectionSetting)
        {
            OBSWebSocketService = obsWebSocketService;
            Argument = argument;
            this.ConnectionSetting = connectionSetting;
        }

        public OBSWebSocketService OBSWebSocketService { get; private set; }
        public string Argument { get; }
        public ConnectionSetting ConnectionSetting { get; }
    }
}
