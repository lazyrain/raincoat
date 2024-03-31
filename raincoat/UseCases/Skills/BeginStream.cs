namespace raincoat.UseCases.Skills
{
    internal class BeginStream : IUseCase<SkillInputPack, SkillOutputPack>
    {
        /// <summary>
        /// OBSの配信開始
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SkillOutputPack Execute(SkillInputPack input)
        {
            input.OBSWebSocketService.WebSocket.StartStream();

            return new SkillOutputPack();
        }
    }
}
