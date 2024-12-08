using UnityEngine;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketReceiver : MonoBehaviour
{
    private ClientWebSocket _webSocket;

    private async void Start()
    {
        _webSocket = new ClientWebSocket();
        await ConnectToWebSocket();
    }

    private async Task ConnectToWebSocket()
    {
        try
        {
            // Connect to the Python WebSocket server
            await _webSocket.ConnectAsync(new System.Uri("ws://localhost:8766"), CancellationToken.None);
            Debug.Log("WebSocket connected!");

            // Buffer for incoming data
            var buffer = new byte[1024];

            // Continuously receive data
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new System.ArraySegment<byte>(buffer), CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.Log($"Received: {message}");

                // Optional: Process the data
                ProcessData(message);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"WebSocket Error: {e.Message}");
        }
    }

    private void ProcessData(string data)
    {
        // Add any logic to handle incoming data here
        Debug.Log($"Processing data: {data}");
    }

    private void OnApplicationQuit()
    {
        _webSocket?.Dispose();
    }
}
