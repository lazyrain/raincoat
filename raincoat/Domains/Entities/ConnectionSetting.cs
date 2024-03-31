namespace raincoat.Domains.Entities
{
    public class ConnectionSetting
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
    }
}
