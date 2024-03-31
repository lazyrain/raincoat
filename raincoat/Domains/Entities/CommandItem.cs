using raincoat.Domains.ValueObjects;

namespace raincoat.Domains.Entities
{
    public class CommandItem
    {

        public SkillType SkillType;
        public string Caption;

        public CommandItem(SkillType skillType, string caption)
        {
            this.SkillType = skillType;
            this.Caption = caption;
        }
    }
}
