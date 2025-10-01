using Moq;
using NUnit.Framework;
using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.UseCases.Skills;

namespace raincoat.Tests
{
    [TestFixture]
    public class SkillInputPackTests
    {
        private Mock<IOBSWebSocketService> _mockObsWebSocketService;

        [SetUp]
        public void Setup()
        {
            _mockObsWebSocketService = new Mock<IOBSWebSocketService>();
        }

        // OK Pattern Test
        [Test]
        public void Constructor_WithNonNullConnectionSetting_SetsProperty()
        {
            // Arrange
            var expectedConnectionSetting = new ConnectionSetting("192.168.1.1", 5555, "test_password");
            var argument = "test_argument";

            // Act
            var skillInputPack = new SkillInputPack(
                _mockObsWebSocketService.Object,
                argument,
                expectedConnectionSetting);

            // Assert
            Assert.That(skillInputPack.ConnectionSetting, Is.EqualTo(expectedConnectionSetting));
            Assert.That(skillInputPack.Argument, Is.EqualTo(argument));
            Assert.That(skillInputPack.OBSWebSocketService, Is.EqualTo(_mockObsWebSocketService.Object));
        }

        // NG Pattern Test
        [Test]
        public void Constructor_WithNullConnectionSetting_SetsDefault()
        {
            // Arrange
            var argument = "test_argument";
            var expectedDefaultConnectionSetting = new ConnectionSetting("localhost", 4444, string.Empty);


            // Act
            var skillInputPack = new SkillInputPack(
                _mockObsWebSocketService.Object,
                argument,
                null); // Pass null for ConnectionSetting

            // Assert
            Assert.That(skillInputPack.ConnectionSetting, Is.Not.Null);
            Assert.That(skillInputPack.ConnectionSetting, Is.EqualTo(expectedDefaultConnectionSetting));
            Assert.That(skillInputPack.Argument, Is.EqualTo(argument));
            Assert.That(skillInputPack.OBSWebSocketService, Is.EqualTo(_mockObsWebSocketService.Object));
        }
    }
}
