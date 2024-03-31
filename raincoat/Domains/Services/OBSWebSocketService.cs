using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;

namespace raincoat.Domains.Services
{
    public class OBSWebSocketService
    {
        public OBSWebsocket WebSocket { get; }

        public OBSWebSocketService()
        {
            WebSocket = new OBSWebsocket();
        }

        public string? HostAddress { get; private set; }
        public int? Port { get; private set; }
        public string? Password { get; private set; }

        public void Connect(string hostAddress, int port, string password)
        {
            this.HostAddress = hostAddress;
            this.Port = port;
            this.Password = password;

            Connect();
        }

        public void Connect()
        {
            try
            {
                if (string.IsNullOrEmpty(this.HostAddress))
                {
                    throw new ArgumentException("HostAddressが設定されていません。");
                }

                if (this.Port == null)
                {
                    throw new ArgumentException("Portが設定されていません。");
                }

                this.WebSocket.ConnectAsync($"ws://{this.HostAddress}:{this.Port}", this.Password);
            }
            catch (Exception ex)
            {
                throw new Exception($"OBSへの接続に失敗しました: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            WebSocket.Disconnect();
        }

        public void OnConnected(EventHandler eventHandler)
        {
            WebSocket.Connected += eventHandler;
        }

        public void OnDisconnected(EventHandler<ObsDisconnectionInfo> eventHandler)
        {
            WebSocket.Disconnected += eventHandler;
        }

    }
}
