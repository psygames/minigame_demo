using UnityEngine;
using Fleck;

public class Server : MonoBehaviour
{
    WebSocketServer server;

    void Start()
    {
        WebSocketServerStart();
    }

    private void OnDestroy()
    {
        server.Dispose();
    }


    private void WebSocketServerStart()
    {
        var address = "ws://0.0.0.0:8888";
        Debug.Log($"Web Socket Server Start At: {address}");
        server = new WebSocketServer(address);
        server.Start(socket =>
        {
            socket.OnOpen = () => Debug.Log($"Socket({socket.ConnectionInfo.ClientIpAddress}) Open!");
            socket.OnClose = () => Debug.Log($"Socket({socket.ConnectionInfo.ClientIpAddress}) Close!");
            socket.OnMessage = message => socket.Send(message);
        });
    }
}
