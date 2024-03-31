namespace raincoat.UseCases.Skills
{
    internal class ChangeScene : IUseCase<SkillInputPack, SkillOutputPack>
    {
        /// <summary>
        /// OBSのシーン切り替え
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SkillOutputPack Execute(SkillInputPack input)
        {
            input.OBSWebSocketService.WebSocket.SetCurrentProgramScene(input.Argument);

            return new SkillOutputPack();
        }
    }
}
