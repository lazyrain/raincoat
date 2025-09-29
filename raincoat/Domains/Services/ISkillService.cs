using raincoat.Domains.Entities;
using raincoat.Domains.ValueObjects;

namespace raincoat.Domains.Services
{
    public interface ISkillService
    {
        void Execute(
            SkillType skillType,
            string argument,
            ConnectionSetting connectionSetting,
            IOBSWebSocketService obsService);
    }
}
