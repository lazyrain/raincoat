using System;

namespace raincoat.Domains.Entities
{
    public class ConnectionSetting : IEquatable<ConnectionSetting>
    {
        public ConnectionSetting() : this("localhost", 4444, string.Empty)
        {
        }

        public ConnectionSetting(string hostAddress, int port, string password)
        {
            this.HostAddress = hostAddress;
            this.Port = port;
            this.Password = password;
        }

        public string HostAddress { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

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

            return HostAddress == other.HostAddress && Port == other.Port && Password == other.Password;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ConnectionSetting);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HostAddress, Port, Password);
        }
    }
}