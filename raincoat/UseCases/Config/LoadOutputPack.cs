using raincoat.Domains.Entities;

namespace raincoat.UseCases.Config
{
    public class LoadOutputPack : IOutputPack
    {
        public ConfigData ConfigData { get; }

        public LoadOutputPack(ConfigData configData)
        {
            this.ConfigData = configData;
        }
    }
}
