using Newtonsoft.Json;
using System.Diagnostics;

namespace raincoat.UseCases.Config
{
    public class Save : IUseCase<SaveInputPack, SaveOutputPack>
    {
        public SaveOutputPack Execute(SaveInputPack input)
        {
            var json = JsonConvert.SerializeObject(input.ConfigData);
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(appDirectory, "config.json");

            File.WriteAllText(filePath, json);
            Debug.WriteLine(json);

            return new SaveOutputPack(json);
        }
    }
}
