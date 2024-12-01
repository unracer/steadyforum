using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace steadyforum.Server.Controllers
{
    public class WebSocketConnectionManager : Controller
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, WebSocket>> poollist = new ConcurrentDictionary<string, ConcurrentDictionary<Guid, WebSocket>>();
        /*transfer this to AddSocket. for create everytime new pool*/
        

        public WebSocket AddSocket(WebSocket socket, string chatname)
        {
            var pool = poollist.GetOrAdd(chatname, new ConcurrentDictionary<Guid, WebSocket>());

            var socketId = Guid.NewGuid();
            pool._socket.TryAdd(socketId, socket);
            return socketId;           
        }

        public WebSocket? GetSocket(Guid socketId)
        {
            poollist.TryGetValue(chatname)._socket.TryGetValue(socketId, out var socket);
            return socket;
        }

        public ConcurrentDictionary<Guid, WebSocket>.ValueCollection GetAllSockets()
        { 
            return _sockets.Values;
        }

        public Guid? GetSocketId(WebSocket socket)
        {
            foreach (var (key, value) in _sockets)
            {
                if (value == socket)
                {
                    return key;
                }
            }
            return null;
        }

        public void RemoveSocket(Guid socketId)
        {
            _sockets.TryRemove(socketId, out _);
        }
    }
}
