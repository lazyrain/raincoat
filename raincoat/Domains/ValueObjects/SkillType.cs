using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace raincoat.Domains.ValueObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillType
    {
        /// <summary>
        /// �ݒ�Ȃ�
        /// </summary>
        None = 0,
        /// <summary>
        /// �V�[���ύX
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
        /// �v���O�������s
        /// </summary>
        RunProgram = 4,
        /// <summary>
        /// �L�[����
        /// </summary>
        KeyStroke = 5,
        /// <summary>
        /// �t�B���^�[�A�N�e�B�u
        /// </summary>
        ActiveFilter = 6,
    }
}