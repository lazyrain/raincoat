using raincoat.Domains.Entities;
using raincoat.Domains.Services;

namespace raincoat.UseCases.Triggers
{
    public class MonitorActiveWindowInputPack : IInputPack
    {
        public MonitorActiveWindowInputPack(ConfigData config, IActiveWindowService windowService, ISkillService skillService)
        {
            Config = config;
            WindowService = windowService;
            SkillService = skillService;
        }

        public ConfigData Config { get; }
        public IActiveWindowService WindowService { get; }
        public ISkillService SkillService { get; }
    }
}
