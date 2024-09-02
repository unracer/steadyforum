using System;
using System.Collections.Generic;
using System.Linq;
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
   /* [Route("api/[controller]")]*/
    [Route("api/")]
    public class ChatController : ControllerBase
    {
        private readonly steadyforumServerContext _context;

        public ChatController(steadyforumServerContext context)
        {
            _context = context;
        }

        // GET: Chat/Details/5
        /*[HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chat
                .FirstOrDefaultAsync(m => m.id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }*/

        // POST: Chat/List
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

            // remove password from output
            return Ok("[ "+ (Regex.Replace(user.Chatlist, @"\W+key\W+\w+", ""))+" ]");
        }

        // POST: Chat/ContentOldApi
        /*Non web socket*/
        [HttpGet("Chat/{sessionid}/{chatname}/{lastmessage}")]
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
                if ( chat == null || chat.Userlist == null || user.Uname == null) { return BadRequest("{ \"status\" : \"denied chat or not exist ;)\"} "); }

            if ( !user.Chatlist.Contains(chatname) ) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }
            if ( !chat.Userlist.Contains(user.Uname) ) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }

            // sanitaze chatname by regexp

            if (lastmessage <= 0)
            {
                chat = await _context
                    .Chat
                    .Where(s => s.Chatname == chatname)
                    .FirstOrDefaultAsync();
                    if ( chat == null ) { return Problem("null #sert54yh4"); }

                return (IActionResult) await _context
                    .Chatcontent
                    .Where(s => s.Idcontent == chat.Idcontent)
                    .ToListAsync();
            } 
            
            if (lastmessage > 0)
            {
                chat = await _context
                    .Chat
                    .Where(s => s.Chatname == chatname)
                    .FirstOrDefaultAsync();
                    if (chat == null) { return Problem("null #edrs55v"); }

                return (IActionResult)await _context
                    .Chatcontent
                    .Where(s => s.Idcontent == chat.Idcontent && s.id > lastmessage)
                    .ToListAsync();
            }

            return BadRequest("dont know why : check #57846232738239 line");
        }

        // POST: Chat/Create
        [HttpGet("Login/{sessionid}/{chatname}/{passwordhash}")]
        public async Task<IActionResult> LoginChat(string sessionid, string chatname,string passwordhash)
        {

            if (_context.User == null || _context.Chat == null ) 
            { 
                return NotFound("null arg"); 
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();
                if (user == null) { return BadRequest(" expired session "); }

            var foundchat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();

            if (foundchat == null) {

                var pastValueChat = new Chat
                {
                    Chatname = chatname,
                    Passwordhash = passwordhash,
                    Userlist = user.Uname + " , "
                };

                if (ModelState.IsValid)
                {
                    _context.Add(pastValueChat);
                    await _context.SaveChangesAsync();
                }
            }

            var createdchat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();
                if (createdchat == null || createdchat.Passwordhash != passwordhash) { return Problem(" denied chat or cant create >_< "); }

            // add access user.chatlist and chat.userlist

            var trackedUser = _context.User.Find(user.id);
            if (trackedUser != null)
            {
                trackedUser.Uname = user.Uname;
                trackedUser.Passwordhash = user.Passwordhash;
                trackedUser.Chatlist = user.Chatlist + ",{\"name\":\"" + chatname + "\",\"key\":\"" + passwordhash + "\"}"; // <space> for sec. bypass
                trackedUser.Sessionid = user.Sessionid;
                trackedUser.SessionCreate = user.SessionCreate;
                _context.SaveChanges();
            }

            var trackedChat = _context.Chat.Find(createdchat.id);
            if (trackedChat != null)
            {
                trackedChat.Chatname = createdchat.Chatname;
                trackedChat.Passwordhash = createdchat.Passwordhash;
                trackedChat.Userlist = createdchat.Userlist + ",{\"uname\":\"" + user.Uname + "\"}"; // <space> for sec. bypass
                trackedChat.Idcontent = createdchat.Idcontent;
                _context.SaveChanges();
            }

            /*if (ModelState.IsValid)
            {
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }*/
            return Ok(createdchat);
        }

        // GET: Chat/Edit/5
        /* [HttpGet]
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var chat = await _context.Chat.FindAsync(id);
             if (chat == null)
             {
                 return NotFound();
             }
             return View(chat);
         }*/
        // POST: Chat/Edit/5 /*Non web socket*/
        [HttpPost("Chat/")]
        public async Task<IActionResult> CreateChatContentNonWs(string sessionid, string chatname, [Bind("Readed,Date,Uname,Text,Meadiapath,Geo")] Chatcontent chatcontent) /* ? how encrypt message*/
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // check access
                    var user = await _context
                        .User
                        .Where(s => s.Sessionid == sessionid)
                        .FirstOrDefaultAsync();
                        if (user == null || user.Chatlist == null) { return BadRequest(" expired session "); }

                    var chat = await _context
                        .Chat
                        .Where(s => s.Chatname == chatname)
                        .FirstOrDefaultAsync();
                        if (chat == null || chat.Userlist == null || user.Uname == null ) { return BadRequest("{ \"status\" : \"denied chat or not exist ;)\"} "); }

                    if (!user.Chatlist.Contains(chatname)) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }
                    if (!chat.Userlist.Contains(user.Uname)) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }

                    // create
                    var chatidcontent = await _context
                       .Chat
                       .Where(s => s.Chatname == chatname)
                       .FirstOrDefaultAsync();
                        if (chatidcontent == null) { return Problem(); }

                    _context.Chatcontent.Add(new Chatcontent { 
                        Idcontent = chatidcontent.Idcontent,
                        Readed = chatcontent.Readed,
                        Date = chatcontent.Date,
                        Uname = chatcontent.Uname,
                        Text = chatcontent.Text,
                        Mediapath = chatcontent.Mediapath,
                        Geo = chatcontent.Geo
                    });
                    var createdchatid = await _context.SaveChangesAsync();

                    /* reference text to another table */

                    /*var createdchat = await _context
                        .Chat
                        .Where(s => s.id == createdchatid)
                        .FirstOrDefaultAsync();
                        if ( createdchat == null ) { return Problem(); }

                    _context.Chatcontent.Add(new Chatcontent { idcontent = createdchat.idcontent});
                    await _context.SaveChangesAsync();*/

                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!ChatExists(chatcontent.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                }
                return Ok();
            }
            return BadRequest();
        }

        // GET: Chat/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chat = await _context.Chat
                .FirstOrDefaultAsync(m => m.id == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }*/

        // POST: Chat/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chat = await _context.Chat.FindAsync(id);
            if (chat != null)
            {
                _context.Chat.Remove(chat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool ChatExists(int id)
        {
            return _context.Chat.Any(e => e.id == id);
        }
    }
}
