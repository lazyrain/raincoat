using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.Infrastructures.Adapters;
using System.IO.Ports;

namespace raincoat.Tests
{
    public class SerialPortServiceTests
    {
        [Test]
        public void DataReceived_WhenFullLineIsReceived_ShouldInvokeOnReceivedCallback()
        {
            // Arrange
            var mockSerialPort = new Mock<ISerialPortWrapper>();
            var receivedKeyStates = new List<KeyState>();
            var service = new SerialPortService(mockSerialPort.Object, (keyStates) =>
            {
                if (keyStates != null) receivedKeyStates.AddRange(keyStates);
            });

            var testData = new List<KeyState> { new KeyState { Button = "SW1", State = "1" } };
            var jsonData = JsonConvert.SerializeObject(testData) + "\n";

            // Act
            mockSerialPort.Setup(p => p.ReadExisting()).Returns(jsonData);
            mockSerialPort.Raise(p => p.DataReceived += null, new object[] { null, null });

            // Assert
            Assert.That(receivedKeyStates.Count, Is.EqualTo(1));
            Assert.That(receivedKeyStates[0].Button, Is.EqualTo("SW1"));
        }

        [Test]
        public void DataReceived_WhenChunkedDataIsReceived_ShouldInvokeOnReceivedCallbackAfterAllChunksAreReceived()
        {
            // Arrange
            var mockSerialPort = new Mock<ISerialPortWrapper>();
            var receivedKeyStates = new List<KeyState>();
            var service = new SerialPortService(mockSerialPort.Object, (keyStates) =>
            {
                if (keyStates != null) receivedKeyStates.AddRange(keyStates);
            });

            var testData = new List<KeyState> { new KeyState { Button = "SW1", State = "1" } };
            var jsonData = JsonConvert.SerializeObject(testData) + "\n";
            var chunk1 = jsonData.Substring(0, 10);
            var chunk2 = jsonData.Substring(10);

            // Act
            mockSerialPort.Setup(p => p.ReadExisting()).Returns(chunk1);
            mockSerialPort.Raise(p => p.DataReceived += null, new object[] { null, null });

            // Assert after first chunk
            Assert.That(receivedKeyStates.Count, Is.EqualTo(0));

            // Act with second chunk
            mockSerialPort.Setup(p => p.ReadExisting()).Returns(chunk2);
            mockSerialPort.Raise(p => p.DataReceived += null, new object[] { null, null });

            // Assert after second chunk
            Assert.That(receivedKeyStates.Count, Is.EqualTo(1));
            Assert.That(receivedKeyStates[0].Button, Is.EqualTo("SW1"));
        }
    }
}
