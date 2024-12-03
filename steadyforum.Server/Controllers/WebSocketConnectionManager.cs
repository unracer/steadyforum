using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace steadyforum.Server.Controllers
{
    public class WebSocketConnectionManager : Controller
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _socketManager = new ConcurrentDictionary<Guid, WebSocket>();
        private readonly ConcurrentDictionary<Guid, string> _socketPoolManager = new ConcurrentDictionary<Guid, string>();
        public Guid AddSocket(WebSocket socket, string chatname)
        {
            var socketId = Guid.NewGuid();
            _socketManager.TryAdd(socketId, socket);
            _socketPoolManager.TryAdd(socketId, chatname);
            return socketId;           
        }

        public WebSocket? GetSocket(Guid socketId)
        {
            _socketManager.TryGetValue(socketId, out var socket);
            return socket;
        }

        public Collection<WebSocket> GetGroupSocket(string requestchatname)
        {
            /*
            request         pool                    soket
            --------------------------------------------------------------
            requestchatname poolguid - poolchatname socketguid - socketid
            --------------------------------------------------------------
            chatname        1 - chatname            1 - socket1
            chatname        2 - chatname            2 - socket2             
             */
            Collection<WebSocket> result = new Collection<WebSocket>();

            foreach (var (poolguid, poolchatname) in _socketPoolManager)
            {
                foreach (var (socketguid, socketid) in _socketManager)
                {
                    if (poolchatname == requestchatname && poolguid == socketguid)
                    {
                        result.Add(socketid);
                    }
                }
            }
            return result;
        }

        public Guid? GetSocketId(WebSocket socket)
        {
            foreach (var (key, value) in _socketManager)
            {
                if (value == socket)
                {
                    return key;
                }
            }
            return null;
        }

        public Guid? GetPool(string chatname)
        {
            foreach (var (key, value) in _socketPoolManager)
            {
                if (value == chatname)
                {
                    return key;
                }
            }
            return null;
        }

        public void RemoveSocket(Guid socketId)
        {
            _socketManager.TryRemove(socketId, out _);
        }
    }
}