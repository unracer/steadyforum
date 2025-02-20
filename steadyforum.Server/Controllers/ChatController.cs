using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using steadyforum.Server.Data;
using steadyforum.Server.Model;
using static System.Net.Mime.MediaTypeNames;

namespace steadyforum.Server.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class Take : ControllerBase
    {
        private readonly steadyforumServerContext _context;

        public Take(steadyforumServerContext context)
        {
            _context = context;
        }

        [HttpGet("List/{sessionid}")]
        public async Task<IActionResult> List(string sessionid)
        {
            if (_context.Chat == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();
            if (user == null || user.Chatlist == null) { return BadRequest("{ \"status\" : \" expired session \"} "); }

            return Ok(user.Chatlist);
        }

        [HttpGet("Login/{sessionid}/{chatname}/{passwordhash}")]
        public async Task<IActionResult> LoginChat(string sessionid, string uniquename)
        {

            if (_context.User == null || _context.Chat == null)
            {
                return NotFound("null arg");
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();
            if (user == null) { return BadRequest(" expired session "); }

            

            if (_context
                .Chat
                .Where(s => s.Chatname == uniquename)
                .FirstOrDefaultAsync() 
                == null)
            {
                //sanitize spec symb
                //cant be lower then 20
                var pastValue = new Chat
                {
                    Chatname = uniquename,
                };

                if (ModelState.IsValid)
                {
                    _context.Add(pastValue);
                    await _context.SaveChangesAsync();
                }
            }

            var createdValue = await _context
                .Chat
                .Where(s => s.Chatname == uniquename)
                .FirstOrDefaultAsync();
            if (createdValue == null) { return Problem(" #h9erpn357 "); }

            _context
                .User
                .Where(q => q.Id == user.Id)
                .ExecuteUpdate(f =>
                    f.SetProperty(q => q.Chatlist, q => q.Chatlist + ",{\"" + uniquename + "\"}"));

            return Ok(createdValue);
        }

        // POST: Chat/ContentOldApi Non web socket
        [HttpGet("Content/{sessionid}/{chatname}/{lastmessage}")]
        public async Task<IActionResult> GetChatContentNonWS(string sessionid, string chatname, int lastmessage)
        {
            if (_context.Chat == null || _context.User == null)
            {
                return NotFound("null arg");
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();
            if (user == null || user.Chatlist == null) { return BadRequest("{ \"status\" : \"expired session\"} "); }

            var chat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();
            if (chat == null) { return BadRequest("{ \"status\" : \" not exist >_< \"} "); }

            if (lastmessage <= 0)
            {
                var response = await _context
                    .Chatcontent
                    .Where(s => s.Idcontent == chat.Idcontent)
                    .ToListAsync();

                return Ok(response);
            }

            if (lastmessage > 0)
            {
                var response = await _context
                    .Chatcontent
                    .Where(s => s.Idcontent == chat.Idcontent && s.Id > lastmessage)
                    .ToListAsync();

                return Ok(response);
            }

            return BadRequest("{ \"status\" : \" #ihb909e5jg \"} ");
        }

        
        [HttpPost("Content/{sessionid}/{chatname}")]
        public async Task<IActionResult> CreateContentNonWs(string sessionid, string uniquechatname, Chatcontent content) 
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var chat = await _context
                .Chat
                .Where(s => s.Chatname == uniquechatname)
                .FirstOrDefaultAsync();                
            if (chat == null) { return Problem("#iuguhdg8u"); }

            content.Idcontent = chat.Idcontent;

            _context.Chatcontent.Add(content);
            var createdchatid = await _context.SaveChangesAsync();

            return Ok();
        }

        //private readonly ChatWebSocketHandler _webSocketHandler;

        //public Take(ChatWebSocketHandler webSocketHandler)
        //{
        //    _webSocketHandler = webSocketHandler;
        //}

        // POST: Chat/Edit/5 /*Non web socket*/
        //[HttpGet("Content/{sessionid}/{chatname}")]
        //public async Task<IActionResult> ChatWS(string sessionid, string chatname) /* ? how encrypt message*/
        //{
        //    if (!ModelState.IsValid) { return BadRequest(); }

        //    // check access
        //    var user = await _context
        //        .User
        //        .Where(s => s.Sessionid == sessionid)
        //        .FirstOrDefaultAsync();
        //    if (user == null || user.Chatlist == null) { return BadRequest(" expired session "); }

        //    var chat = await _context
        //        .Chat
        //        .Where(s => s.Chatname == chatname)
        //        .FirstOrDefaultAsync();
        //    if (chat == null || chat.Userlist == null || user.Uname == null) { return BadRequest("{ \"status\" : \"denied chat or not exist ;)\"} "); }

        //    if (!user.Chatlist.Contains(chatname)) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }
        //    if (!chat.Userlist.Contains(user.Uname)) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }

        //    //preperation

        //    if (!HttpContext.WebSockets.IsWebSocketRequest)
        //    {
        //        return BadRequest("Websocket is not support");                
        //    }

        //    /*filter socket by chatname for broadcast*/
        //    var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        //    await _webSocketHandler.HandlerWebSocket(HttpContext, webSocket, chatname);

        //    return Ok("closed");
        //}
    }
}
