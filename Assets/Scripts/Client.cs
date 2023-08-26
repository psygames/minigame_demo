using UnityEngine;
using UnityWebSocket;
using System.Collections;
using MessagePack;

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
        client.OnClose += (e, d) => Debug.Log("Client Close!");
        client.OnMessage += (e, d) =>
        {
            var msg = MessagePackSerializer.Deserialize<MsgTest>(d.RawData);
            Debug.Log($"Client OnMessage: {msg.info}");
        };
        StartCoroutine(ClientTest());
    }

    IEnumerator ClientTest()
    {
        var msg = new MsgTest();
        var num = 1;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (client.ReadyState == WebSocketState.Open)
            {
                msg.num = num++;
                msg.date = System.DateTime.Now.ToString();
                var bytes = MessagePackSerializer.Serialize(msg);
                client.SendAsync(bytes);
            }
        }
    }
}
