using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;

namespace raincoat.Domains.Services
{
    public class OBSWebSocketService
    {
        private OBSWebsocket WebSocket { get; }

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

                if (!this.WebSocket.IsConnected)
                {
                    this.WebSocket.ConnectAsync($"ws://{this.HostAddress}:{this.Port}", this.Password);
                }
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

        private void InvokeWebSocketCommand(Action action)
        {
            this.Connect();
            if (this.WebSocket.IsConnected)
            {
                action.Invoke();
            }
        }

        public void StartStream()
        {
            this.InvokeWebSocketCommand(() => this.WebSocket.StartStream());
        }

        public void StopStream()
        {
            this.InvokeWebSocketCommand(() => this.WebSocket.StopStream());
        }

        public void SetCurrentProgramScene(string sceneName)
        {
            this.InvokeWebSocketCommand(() => this.WebSocket.SetCurrentProgramScene(sceneName));
        }

        public void ActiveFilter(string filterName)
        {
            this.InvokeWebSocketCommand(() =>
            {
                var currentProgramScene = this.WebSocket.GetCurrentProgramScene();
                this.WebSocket.SetSourceFilterEnabled(currentProgramScene, filterName, true);
            });
        }
    }
}
