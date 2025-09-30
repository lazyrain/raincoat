using Moq;
using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.Domains.ValueObjects;
using raincoat.UseCases.Triggers;

namespace raincoat.Tests
{
    [TestFixture]
    public class MonitorActiveWindowTests
    {
        private Mock<IActiveWindowService> _mockActiveWindowService;
        private Mock<ISkillService> _mockSkillService;
        private Mock<IOBSWebSocketService> _mockOBSWebSocketService;
        private ConfigData _configData;
        private MonitorActiveWindow _monitorActiveWindow;
        private ConnectionSetting _connectionSetting;

        [SetUp]
        public void Setup()
        {
            _mockActiveWindowService = new Mock<IActiveWindowService>();
            _mockSkillService = new Mock<ISkillService>();
            _mockOBSWebSocketService = new Mock<IOBSWebSocketService>();
            _connectionSetting = new ConnectionSetting("localhost", 4444, "password");

            _configData = new ConfigData(
                _connectionSetting,
                new List<KeyCommandPair>()
            );

            _monitorActiveWindow = new MonitorActiveWindow(); // No constructor arguments now
        }

        [TearDown]
        public void Teardown()
        {
            _monitorActiveWindow.Stop(); // Ensure the background task is stopped
        }

        [Test]
        public async Task Execute_ShouldNotTriggerSkill_WhenWindowTitleDoesNotMatch()
        {
            // Arrange
            var keyCommand = new KeyCommandPair(
                "btn1",
                "TestButton",
                SkillType.BeginStream,
                "arg1",
                true,
                "Target Window");
            _configData.KeyCommands.Add(keyCommand);

            _mockActiveWindowService.Setup(s => s.GetActiveWindowTitle())
                .Returns("Completely Different Window");

            var inputPack = new MonitorActiveWindowInputPack(
                _configData,
                _mockActiveWindowService.Object,
                _mockSkillService.Object,
                _mockOBSWebSocketService.Object
            );

            // Act
            _monitorActiveWindow.Execute(inputPack);
            await Task.Delay(1100);

            // Assert
            _mockSkillService.Verify(s => s.Execute(
                It.IsAny<SkillType>(),
                It.IsAny<string>(),
                It.IsAny<ConnectionSetting>(),
                It.IsAny<IOBSWebSocketService>()), Times.Never);
        }

        [Test]
        public async Task Execute_ShouldNotTriggerSkill_WhenIsWindowTriggerIsFalse()
        {
            // Arrange
            var keyCommand = new KeyCommandPair("btn1", "TestButton", SkillType.BeginStream, "arg1", false, "Target Window"); // IsWindowTrigger is false
            _configData.KeyCommands.Add(keyCommand);

            _mockActiveWindowService.Setup(s => s.GetActiveWindowTitle())
                .Returns("My Target Window Application"); // Title matches, but IsWindowTrigger is false

            var inputPack = new MonitorActiveWindowInputPack(
                _configData,
                _mockActiveWindowService.Object,
                _mockSkillService.Object,
                _mockOBSWebSocketService.Object
            );

            // Act
            _monitorActiveWindow.Execute(inputPack);
            await Task.Delay(1100);

            // Assert
            _mockSkillService.Verify(s => s.Execute(
                It.IsAny<SkillType>(),
                It.IsAny<string>(),
                It.IsAny<ConnectionSetting>(),
                It.IsAny<IOBSWebSocketService>()), Times.Never);
        }

        [Test]
        public async Task Execute_ShouldStopMonitoringWhenStopIsCalled()
        {
            // Arrange
            _mockActiveWindowService.Setup(s => s.GetActiveWindowTitle())
                .Returns("Any Window");

            var inputPack = new MonitorActiveWindowInputPack(
                _configData,
                _mockActiveWindowService.Object,
                _mockSkillService.Object,
                _mockOBSWebSocketService.Object
            );

            // Act
            _monitorActiveWindow.Execute(inputPack); // Start monitoring
            await Task.Delay(1100); // Let it run for a bit

            // Assert that monitoring was active
            _mockActiveWindowService.Verify(s => s.GetActiveWindowTitle(), Times.AtLeastOnce());

            // Clear invocations and stop
            _mockActiveWindowService.Invocations.Clear();
            _monitorActiveWindow.Stop(); // Stop monitoring
            await Task.Delay(1100); // Give it time to stop

            // Assert that monitoring has stopped
            _mockActiveWindowService.Verify(s => s.GetActiveWindowTitle(), Times.Never());
        }

        [Test]
        public async Task Execute_ShouldTriggerSkill_WhenWindowTitleMatches()
        {
            // Arrange
            var keyCommand = new KeyCommandPair(
                "btn1",
                "TestButton",
                SkillType.BeginStream,
                "arg1",
                true,
                "Target Window"); // Regex pattern
            _configData.KeyCommands.Add(keyCommand);

            _mockActiveWindowService.Setup(s => s.GetActiveWindowTitle())
                .Returns("This is the Target Window"); // This title matches the regex

            var inputPack = new MonitorActiveWindowInputPack(
                _configData,
                _mockActiveWindowService.Object,
                _mockSkillService.Object,
                _mockOBSWebSocketService.Object
            );

            // Act
            _monitorActiveWindow.Execute(inputPack);
            await Task.Delay(1100); // Allow time for the monitor to trigger

            // Assert
            _mockSkillService.Verify(s => s.Execute(
                keyCommand.SkillType,
                keyCommand.Argument,
                _connectionSetting,
                _mockOBSWebSocketService.Object), Times.Once);
        }
    }
}
