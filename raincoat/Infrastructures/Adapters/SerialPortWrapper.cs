using System.IO.Ports;
using raincoat.Infrastructures.Adapters;

namespace raincoat.Domains.Services
{
    public class SerialPortWrapper : ISerialPortWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialPortWrapper(string portName, int baudRate)
        {
            if (string.IsNullOrEmpty(portName))
            {
                throw new ArgumentNullException(nameof(portName), "ポート名を指定してください。");
            }

            _serialPort = new SerialPort(portName, baudRate)
            {
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 1000,
                WriteTimeout = 1000,
            };
        }

        public bool IsOpen => _serialPort.IsOpen;

        public int ReadTimeout
        {
            get => _serialPort.ReadTimeout;
            set => _serialPort.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => _serialPort.WriteTimeout;
            set => _serialPort.WriteTimeout = value;
        }

        public event SerialDataReceivedEventHandler DataReceived
        {
            add => _serialPort.DataReceived += value;
            remove => _serialPort.DataReceived -= value;
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public void Close()
        {
            _serialPort.Close();
        }

        public string ReadLine()
        {
            return _serialPort.ReadLine();
        }

        public string ReadExisting()
        {
            return _serialPort.ReadExisting();
        }

        public System.IO.Stream BaseStream => _serialPort.BaseStream;
    }
}
