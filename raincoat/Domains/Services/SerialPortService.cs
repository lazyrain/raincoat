using Newtonsoft.Json;
using raincoat.Domains.Entities;
using System.Diagnostics;
using System.IO.Ports;

namespace raincoat.Domains.Services
{
    public class SerialPortService
    {
        private readonly SerialPort serialPort;
        private readonly Action<IList<KeyState>?> OnReceived;

        public SerialPortService(string portName, int baudRate, Action<IList<KeyState>?> onReceived)
        {
            serialPort = new SerialPort(portName, baudRate)
            {
                ReadTimeout = 500,
                WriteTimeout = 500,
            };
            serialPort.DataReceived += DataReceived;
            this.OnReceived = onReceived;
        }

        public void OpenSerialPort()
        {
            try
            {
                serialPort.Open();
                Debug.WriteLine($"シリアルポートをオープンします。{this.serialPort}");

            }
            catch (Exception ex)
            {
                throw new Exception("シリアルポートのオープン中にエラーが発生しました: " + ex.Message);
            }
        }

        public void CloseSerialPort()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            Debug.WriteLine("受信したデータ: " + data);

            try
            {
                var keyStates = JsonConvert.DeserializeObject<List<KeyState>>(data);

                if (keyStates != null)
                {
                    foreach (var keyState in keyStates)
                    {
                        Debug.WriteLine("ボタン: " + keyState.Button);
                        Debug.WriteLine("ステート: " + keyState.State);
                        Debug.WriteLine("----------");
                    }

                    this.OnReceived.Invoke(keyStates);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("JSONデータの解析中にエラーが発生しました", ex);
            }
        }
    }
}
