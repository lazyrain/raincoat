using Newtonsoft.Json;
using raincoat.Domains.Entities;

namespace raincoat.UseCases.Config
{
    /// <summary>
    /// 設定データを読み込むためのユースケースです。
    /// </summary>
    public class Load : IUseCase<LoadInputPack, LoadOutputPack>
    {
        /// <summary>
        /// 設定データを読み込みます。設定ファイルが存在しない場合、
        /// または設定ファイルが空の場合は新しい設定データを作成します。
        /// </summary>
        /// <param name="input">読み込み処理の入力パック</param>
        /// <returns>読み込み処理の出力パック</returns>
        public LoadOutputPack Execute(LoadInputPack input)
        {
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(appDirectory, "config.json");

            if (!File.Exists(filePath) || string.IsNullOrEmpty(File.ReadAllText(filePath)))
            {
                return new LoadOutputPack(this.CreateAndSaveNewConfigData(filePath));
            }

            var json = File.ReadAllText(filePath);
            var configData = JsonConvert.DeserializeObject<ConfigData>(json);

            if (configData == null)
            {
                return new LoadOutputPack(this.CreateAndSaveNewConfigData(filePath));
            }

            return new LoadOutputPack(configData);
        }

        /// <summary>
        /// 新しい設定データを作成し、それを設定ファイルに保存します。
        /// </summary>
        /// <param name="filePath">設定ファイルのパス</param>
        /// <returns>新しく作成された設定データ</returns>
        private ConfigData CreateAndSaveNewConfigData(string filePath)
        {
            var newConfigData = new ConfigData();  // 適切な初期値で初期化する必要があります。
            var newJson = JsonConvert.SerializeObject(newConfigData);
            File.WriteAllText(filePath, newJson);
            return newConfigData;
        }
    }
}
