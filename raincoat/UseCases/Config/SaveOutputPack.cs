namespace raincoat.UseCases.Config
{
    public class SaveOutputPack : IOutputPack
    {
        public SaveOutputPack(string json)
        {
            this.Json = json;
        }

        public string Json { get; }
    }
}
