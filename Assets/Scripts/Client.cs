using UnityEngine;
using UnityWebSocket;
using System.Collections;

public class Client : MonoBehaviour
{

    WebSocket client;
    void Start()
    {
        StartCoroutine(WebSocketConnect());
    }

    IEnumerator WebSocketConnect()
    {
        yield return new WaitForSeconds(1);
        var address = "ws://127.0.0.1:8888";
        Debug.Log($"Web Socket Connect To: {address}");
        client = new WebSocket(address);
        client.ConnectAsync();
        client.OnOpen += (e, d) => Debug.Log("Client Open!");
        client.OnMessage += (e, d) => Debug.Log($"Client OnMessage: {d.Data}");
        client.OnClose += (e, d) => Debug.Log("Client Close!");
        StartCoroutine(ClientTest());
    }

    IEnumerator ClientTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (client.ReadyState == WebSocketState.Open)
            {
                client.SendAsync("test");
            }
        }
    }
}
