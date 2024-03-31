using OBSWebsocketDotNet;

namespace raincoat.UseCases.Skills
{
    internal class EndStream : IUseCase<SkillInputPack, SkillOutputPack>
    {
        /// <summary>
        /// OBSの配信終了
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SkillOutputPack Execute(SkillInputPack input)
        {
            try
            {
                input.OBSWebSocketService.WebSocket.StopStream();
            }
            catch (ErrorResponseException ex)
            {
                var content = string.Empty;
                if (ex.ErrorCode == 501)
                {
                    content = "配信されていない。";
                }

                MessageBox.Show(
                    $"配信終了できませんでした:{ex.Message}\n{content}",
                    "EndStream",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "配信終了できませんでした:" + ex.Message,
                    "EndStream",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return new SkillOutputPack();
        }
    }
}
