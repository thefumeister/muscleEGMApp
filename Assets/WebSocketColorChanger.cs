using UnityEngine;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketColorChanger : MonoBehaviour
{
    private ClientWebSocket _webSocket;
    private Renderer _renderer;

    [Tooltip("Minimum value for mapping to color.")]
    public float minValue = 0f;

    [Tooltip("Maximum value for mapping to color.")]
    public float maxValue = 100f;

    [Tooltip("Gradient for color mapping.")]
    public Gradient colorGradient;

    private async void Start()
    {
        _webSocket = new ClientWebSocket();
        _renderer = GetComponent<Renderer>();

        try
        {
            Debug.Log("Connecting to WebSocket...");
            await _webSocket.ConnectAsync(new System.Uri("ws://localhost:8766"), CancellationToken.None);
            Debug.Log("WebSocket connected!");

            byte[] buffer = new byte[1024];

            while (_webSocket.State == WebSocketState.Open)
            {
                // Receive data from the WebSocket
                var result = await _webSocket.ReceiveAsync(new System.ArraySegment<byte>(buffer), CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                // Try parsing the data
                if (float.TryParse(message, out float value))
                {
                    Debug.Log($"Received value: {value}");
                    UpdateColor(value);
                }
                else
                {
                    Debug.LogWarning($"Invalid data received: {message}");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"WebSocket connection error: {e.Message}");
        }
    }

    private void UpdateColor(float value)
    {
        // Normalize the value between 0 and 1 based on min and max
        float normalizedValue = Mathf.InverseLerp(minValue, maxValue, value);

        // Get the color from the gradient
        Color newColor = colorGradient.Evaluate(normalizedValue);

        // Apply the color to the GameObject's material
        if (_renderer != null)
        {
            _renderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("No Renderer component found on the GameObject.");
        }
    }

    private void OnApplicationQuit()
    {
        _webSocket?.Dispose();
    }
}
