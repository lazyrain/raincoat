namespace raincoat.Domains.Entities
{
    public class ConfigData
    {
        public ConfigData() : this(new ConnectionSetting(), new List<KeyCommandPair>())
        {
        }

        public ConfigData(ConnectionSetting connectionSetting, List<KeyCommandPair> keyCommands)
        {
            this.ConnectionSetting = new ConnectionSetting(
                connectionSetting.HostAddress,
                connectionSetting.Port,
                connectionSetting.Password);

            this.KeyCommands = new List<KeyCommandPair>();

            foreach (var item in keyCommands)
            {
                this.KeyCommands.Add(new(item.ButtonId, item.ButtonName, item.SkillType, item.Argument));
            }
        }

        public ConnectionSetting ConnectionSetting { get; set; }
        public List<KeyCommandPair> KeyCommands { get; set; }
    }
}
