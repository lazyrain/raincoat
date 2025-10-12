using Newtonsoft.Json;
using raincoat.Domains.Entities;
using raincoat.Infrastructures.Adapters;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;

namespace raincoat.Domains.Services
{
    public class SerialPortService
    {
        private readonly ISerialPortWrapper serialPort;
        private readonly Action<IList<KeyState>?> OnReceived;
        private readonly StringBuilder _receivedDataBuffer = new StringBuilder();

        public SerialPortService(ISerialPortWrapper serialPort, Action<IList<KeyState>?> onReceived)
        {
            this.serialPort = serialPort;
            this.serialPort.ReadTimeout = 500;
            this.serialPort.WriteTimeout = 500;
            this.serialPort.DataReceived += DataReceived;
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
                if (serialPort.BaseStream.CanRead)
                {
                    serialPort.Close();
                }
            }
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadExisting();
                _receivedDataBuffer.Append(data);

                string bufferContent = _receivedDataBuffer.ToString();
                int newlineIndex;
                while ((newlineIndex = bufferContent.IndexOf('\n')) >= 0)
                {
                    string line = bufferContent.Substring(0, newlineIndex).Trim();
                    _receivedDataBuffer.Remove(0, newlineIndex + 1);
                    bufferContent = _receivedDataBuffer.ToString();

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    Debug.WriteLine("受信したデータ: " + line);

                    var keyStates = JsonConvert.DeserializeObject<List<KeyState>>(line);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JSONデータの解析中にエラーが発生しました:\n{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
