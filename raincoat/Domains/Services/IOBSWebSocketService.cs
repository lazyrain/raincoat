using OBSWebsocketDotNet.Communication;

namespace raincoat.Domains.Services
{
    public interface IOBSWebSocketService
    {
        string? HostAddress { get; }
        string? Password { get; }
        int? Port { get; }

        void ActiveFilter(string filterName);
        void Connect();
        void Connect(string hostAddress, int port, string password);
        void Disconnect();
        void OnConnected(EventHandler eventHandler);
        void OnDisconnected(EventHandler<ObsDisconnectionInfo> eventHandler);
        void SetCurrentProgramScene(string sceneName);
        void StartStream();
        void StopStream();
    }
}