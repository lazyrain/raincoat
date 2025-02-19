using raincoat.Domains.ValueObjects;
using System.Data;

namespace raincoat.Infrastructures.Repositories
{
    public class SkillItemsRepository : IRepository
    {
        public DataTable SkillItems { get; private set; } = new("SkillItems");

        public SkillItemsRepository()
        {
            this.SkillItems.Columns.Add("Value", typeof(int));
            this.SkillItems.Columns.Add("Display", typeof(string));

            this.SkillItems.Rows.Add((int)SkillType.None, string.Empty);
            this.SkillItems.Rows.Add((int)SkillType.ChangeScene, "シーン切り替え");
            this.SkillItems.Rows.Add((int)SkillType.BeginStream, "配信開始");
            this.SkillItems.Rows.Add((int)SkillType.EndStream, "配信終了");
            this.SkillItems.Rows.Add((int)SkillType.RunProgram, "パス起動");
            this.SkillItems.Rows.Add((int)SkillType.KeyStroke, "キーを押す");
        }

        public DataGridViewComboBoxColumn GetDataGridViewComboBoxColumn()
        {
            return new()
            {
                //表示する列の名前を設定する
                HeaderText = "コマンド",
                //DataGridViewComboBoxColumnのDataSourceを設定
                DataSource = this.SkillItems,
                //実際の値が"Value"列、表示するテキストが"Display"列とする
                ValueMember = "Value",
                DisplayMember = "Display",
                Name = "Skill",
            };
        }
    }
}
