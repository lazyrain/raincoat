using raincoat.Domains.Entities;
using raincoat.Domains.ValueObjects;
using raincoat.Infrastructures.Repositories;
using raincoat.UseCases.Config;

namespace raincoat
{
    public partial class ButtonSetting : Form
    {
        private string buttonId;

        private readonly SkillItemsRepository skillItemsRepository = new();
        private readonly Save save;
        private readonly Load load;
        private readonly ConfigData config;

        public ButtonSetting()
        {
            InitializeComponent();

            save = new Save();
            load = new Load();
        }

        public ButtonSetting(string buttonId) : this()
        {
            this.buttonId = buttonId;

            this.comboSkillType.Items.Clear();
            this.comboSkillType.DataSource = skillItemsRepository.SkillItems;
            this.comboSkillType.ValueMember = "Value";
            this.comboSkillType.DisplayMember = "Display";

            var output = load.Execute(new LoadInputPack());

            this.config = output.ConfigData;

            var item = this.config.KeyCommands.FirstOrDefault(item => item.ButtonId == buttonId);

            this.textName.Text = item?.ButtonName ?? buttonId;
            this.textArgument.Text = item?.Argument ?? string.Empty;
            this.comboSkillType.SelectedValue = item?.SkillType ?? SkillType.None;
        }

        private void ButtonSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            var items = this.config.KeyCommands.Where(KeyCommandPair => KeyCommandPair.ButtonId == this.buttonId);

            if (!items.Any())
            {
                this.config.KeyCommands.Add(new KeyCommandPair(
                    this.buttonId,
                    string.Empty,
                    SkillType.None,
                    string.Empty));

                items = this.config.KeyCommands.Where(KeyCommandPair => KeyCommandPair.ButtonId == this.buttonId);
            }

            foreach (var item in items)
            {
                item.ButtonName = this.textName.Text;
                item.Argument = this.textArgument.Text;
                item.SkillType = (SkillType)this.comboSkillType.SelectedValue;
            }
            this.save.Execute(new SaveInputPack(this.config));
        }
    }
}
