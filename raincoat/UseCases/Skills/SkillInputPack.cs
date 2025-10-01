using raincoat.Domains.Entities;
using raincoat.Domains.Services;

namespace raincoat.UseCases.Skills
{
    public class SkillInputPack : IInputPack
    {
        public SkillInputPack(IOBSWebSocketService obsWebSocketService, string argument, ConnectionSetting? connectionSetting)
        {
            OBSWebSocketService = obsWebSocketService;
            Argument = argument;
            this.ConnectionSetting = connectionSetting ?? new ConnectionSetting();
        }

        public IOBSWebSocketService OBSWebSocketService { get; private set; }
        public string Argument { get; }
        public ConnectionSetting ConnectionSetting { get; }
    }
}
