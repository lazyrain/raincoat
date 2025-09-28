using raincoat.Domains.Services;

namespace raincoat.Tests
{
    public class ServiceTests
    {
        [Test]
        public void ActiveWindowService_GetActiveWindowTitle_DoesNotCrash()
        {
            // Arrange
            var service = new ActiveWindowService();

            // Act
            // このテストは、テスト実行時にアクティブなウィンドウのタイトルを取得します。
            // そのため、結果は非決定的です。
            // ここでは、メソッドがクラッシュせずに何らかの文字列を返すことだけを検証します。
            var windowTitle = service.GetActiveWindowTitle();

            // Assert
            Assert.That(windowTitle, Is.Not.Null);
            TestContext.Out.WriteLine($"Active window was: {windowTitle}");
        }
    }
}
