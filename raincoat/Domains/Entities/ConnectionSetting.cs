using System;

namespace raincoat.Domains.Entities
{
    public class ConnectionSetting : IEquatable<ConnectionSetting>
    {
        public ConnectionSetting() : this("localhost", 4444, string.Empty, string.Empty, 9600)
        {
        }

        public ConnectionSetting(string hostAddress, int port, string password, string serialPortName, int baudRate)
        {
            this.HostAddress = hostAddress;
            this.Port = port;
            this.Password = password;
            this.SerialPortName = serialPortName;
            this.BaudRate = baudRate;
        }

        public string HostAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string SerialPortName { get; set; }
        public int BaudRate { get; set; }


        public bool Equals(ConnectionSetting? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return HostAddress == other.HostAddress && 
                   Port == other.Port && 
                   Password == other.Password &&
                   SerialPortName == other.SerialPortName &&
                   BaudRate == other.BaudRate;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ConnectionSetting);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HostAddress, Port, Password, SerialPortName, BaudRate);
        }
    }
}