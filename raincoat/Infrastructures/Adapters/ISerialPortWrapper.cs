
using System.IO.Ports;

namespace raincoat.Infrastructures.Adapters
{
    public interface ISerialPortWrapper
    {
        bool IsOpen { get; }
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
        void Open();
        void Close();
        string ReadLine();
        string ReadExisting();
        event SerialDataReceivedEventHandler DataReceived;
        System.IO.Stream BaseStream { get; }
    }
}
