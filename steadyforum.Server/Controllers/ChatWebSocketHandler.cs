using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace steadyforum.Server.Controllers
{
    public class ChatWebSocketHandler : Controller
    {
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly ILogger<ChatWebSocketHandler> _logger;

        public ChatWebSocketHandler(WebSocketConnectionManager connectionManager, ILogger<ChatWebSocketHandler> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task HandlerWebSocket(HttpContext context, WebSocketManager webSocket, chatname)
        {
            var socketId = _connectionManager.AddSocket(webSocket, chatname);
            /*var socketId = _connectionPool.AddSocket(webSocket);*/
            _logger.LogInformation("establish {socketId}");

            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessageAsync(webSocket);
                if (message == null)
                {
                    continue;
                }
                _logger.LogInformation("receive {socketID}");
                await BroadcastMessageAsync(message);
            }
            _connectionManager.RemoveSocket(socketId);
            _logger.LogInformation("closed {socketId}");
        }

        private async Task<string> ReceiveMessageAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.CloseStatus.HasValue)
            {
                return null;
            }
            return System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        private async Task BroadcastMessageAsync(string message)
        {
            foreach (var socket in _connectionManager.GetAllSockets())
            {
                if (socket.Value.State == WebSocketState.Open)
                {
                    await socket.Value.SendAsync(System.Text.Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
