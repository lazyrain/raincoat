using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace raincoat.Domains.ValueObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillType
    {
        /// <summary>
        /// �Ȃ�
        /// </summary>
        None = 0,
        /// <summary>
        /// �V�[���؂�ւ�
        /// </summary>
        ChangeScene = 1,
        /// <summary>
        /// �z�M�J�n
        /// </summary>
        BeginStream = 2,
        /// <summary>
        /// �z�M�I��
        /// </summary>
        EndStream = 3,
        /// <summary>
        /// �p�X�N��
        /// </summary>
        RunProgram = 4,
        /// <summary>
        /// �L�[������
        /// </summary>
        KeyStroke = 5,
        /// <summary>
        /// �t�B���^�[�I��
        /// </summary>
        ActiveFilter = 6,
    }
}