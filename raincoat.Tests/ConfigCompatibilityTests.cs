using Newtonsoft.Json;
using raincoat.Domains.Entities;

namespace raincoat.Tests
{
    public class ConfigCompatibilityTests
    {
        [Test]
        public void OldConfig_CanBeDeserialized_WithoutError()
        {
            // Arrange: IsWindowTriggerとTriggerWindowTitleを含まない古い形式のJSON
            var oldJson = @"{
                ""ConnectionSetting"": {
                    ""HostAddress"": ""localhost"",
                    ""Port"": 4455,
                    ""Password"": ""password""
                },
                ""KeyCommands"": [
                    {
                        ""ButtonId"": ""1"",
                        ""ButtonName"": ""Test Button"",
                        ""SkillType"": ""KeyStroke"",
                        ""Argument"": ""A""
                    }
                ]
            }";

            // Act
            var configData = JsonConvert.DeserializeObject<ConfigData>(oldJson);

            // Assert
            Assert.That(configData, Is.Not.Null);
            Assert.That(configData.KeyCommands, Has.Count.EqualTo(1));

            var command = configData.KeyCommands[0];
            Assert.Multiple(() =>
            {
                Assert.That(command.ButtonId, Is.EqualTo("1"));

                // 新しいプロパティがデフォルト値で初期化されていることを確認
                Assert.That(command.IsWindowTrigger, Is.False);
                Assert.That(command.TriggerWindowTitle, Is.EqualTo(string.Empty));
            });
        }
    }
}
