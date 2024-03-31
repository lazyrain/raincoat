using raincoat.Domains.Entities;

namespace raincoat.UseCases.Config
{
    public class SaveInputPack : IInputPack
    {
        public SaveInputPack(ConfigData configData)
        {
            this.ConfigData = configData;
        }

        public ConfigData ConfigData { get; }
    }
}
