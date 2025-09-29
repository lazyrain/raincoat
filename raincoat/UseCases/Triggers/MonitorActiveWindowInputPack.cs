using raincoat.Domains.Entities;
using raincoat.Domains.Services;

namespace raincoat.UseCases.Triggers
{
    public class MonitorActiveWindowInputPack : IInputPack
    {
        public MonitorActiveWindowInputPack(ConfigData config, IActiveWindowService windowService, ISkillService skillService, OBSWebSocketService obsService)
        {
            Config = config;
            WindowService = windowService;
            SkillService = skillService;
            ObsService = obsService;
        }

        public ConfigData Config { get; }
        public IActiveWindowService WindowService { get; }
        public ISkillService SkillService { get; }
        public OBSWebSocketService ObsService { get; }
    }
}
