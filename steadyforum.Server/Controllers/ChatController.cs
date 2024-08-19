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
            if (_context.Chat == null)
            {
                return NotFound();
            }
            if (_context.User == null)
            {
                return NotFound();
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();

            if (user == null || user.Chatlist == null) { return BadRequest("{ \"status\" : \" expired session \"} "); }

            // clear password
            return Ok("[ "+ (Regex.Replace(user.Chatlist, @"\W+key\W+\w+", ""))+" ]");
        }

        // POST: Chat/ContentOldApi
        /*Non web socket*/
        [HttpGet("Chat/{sessionid}/{chatname}/{lastmessage}")]
        public async Task<IActionResult> GetChatContentNonWS(string? sessionid, string? chatname, string? lastmessageAsString)
        {
            if (_context.Chat == null)
            {
                return NotFound();
            }
            if (_context.User == null)
            {
                return NotFound();
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();

            var chat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();

            // template to return json Problem("{ \"status\" : \" \"} "); 
            if ( user == null || user.Chatlist == null) { return BadRequest("{ \"status\" : \"expired session\"} "); }
            if ( chat == null || chat.Userlist == null || user.Uname == null) { return BadRequest("{ \"status\" : \"denied chat or not exist ;)\"} "); }

            if (chatname == null || lastmessageAsString == null) { return BadRequest("null arg"); }

            if (!user.Chatlist.Contains(chatname) || 
                !chat.Userlist.Contains(user.Uname)
                ) { return Problem("{ \"status\" : \"denied, click <create chat> and login to chat again or call admin\"} "); }

            // check chatname by regexp

            int lastmessage = int.Parse(lastmessageAsString);
            if (lastmessage == 0)
            {
                chat = await _context
                    .Chat
                    .Where(s => s.Chatname == chatname)
                    .FirstOrDefaultAsync();

                // i am hate null checkers
                if (chat == null) { return Problem("null"); }

                return (IActionResult) await _context
                    .Chatcontent
                    .Where(s => s.idcontent == chat.idcontent)
                    .ToListAsync();

            } else if (lastmessage > 0)
            {
                chat = await _context
                    .Chat
                    .Where(s => s.Chatname == chatname)
                    .FirstOrDefaultAsync();

                // i am hate null checkers
                if (chat == null) { return Problem("null"); }

                return (IActionResult)await _context
                    .Chatcontent
                    .Where(s => s.idcontent == chat.idcontent && s.id > lastmessage)
                    .ToListAsync();
            }
            return BadRequest("{ \"status\" : \" invalid arg \"} ");
        }

        // POST: Chat/Create
        [HttpGet("Login/{sessionid}/{chatname}/{passwordhash}")]
        public async Task<IActionResult> LoginChat(string? sessionid, string? chatname,string? passwordhash)
        {

            if (_context.Chat == null)
            {
                return NotFound();
            }
            if (_context.User == null)
            {
                return NotFound();
            }

            if (chatname == null || passwordhash == null) { return BadRequest("null arg"); }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();

            var chat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();

            if (user == null) { return Problem(" expired session "); }

            bool newlyCreated = false;

            if (chat == null) {
                // create

                var pastValueChat = new Chat
                {
                    Chatname = chatname,
                    Passwordhash = passwordhash,
                    Userlist = user.Uname + " , ", // <space> for sec. bypass
                    /*idcontent = 0,*/
                };

                if (ModelState.IsValid)
                {
                    _context.Add(pastValueChat);
                    await _context.SaveChangesAsync();
                }

                // if success
                newlyCreated = true;
            }

            chat = await _context
                .Chat
                .Where(s => s.Chatname == chatname)
                .FirstOrDefaultAsync();

            if (chat == null) { return Problem("cant past"); }

            if (!newlyCreated && chat.Passwordhash != passwordhash) { return Problem(" denied chat or not exist ;) "); }

            // add access user.chatlist and chat.userlist
            var trackedUser = _context.User.Find(user.id);

            if (trackedUser == null) { return BadRequest("ep tvoy maty ya hui znat kak that may be"); }

            trackedUser.Uname = user.Uname;
            trackedUser.Passwordhash = user.Passwordhash;
            trackedUser.Chatlist = user.Chatlist + ",{\"name\":\"" + chatname + "\",\"key\":\""+ passwordhash +"\"}"; // <space> for sec. bypass
            trackedUser.Sessionid = user.Sessionid;
            trackedUser.SessionCreate = user.SessionCreate;
            _context.SaveChanges();

            var trackedChat = _context.Chat.Find(chat.id);

            if (trackedChat == null) { return BadRequest("ep tvoy maty ya hui znat kak that may be"); }

            trackedChat.Chatname = chat.Chatname;
            trackedChat.Passwordhash = chat.Passwordhash;
            trackedChat.Userlist = chat.Userlist + ",{\"uname\":\"" + user.Uname + "\"}"; // <space> for sec. bypass
            trackedChat.idcontent = chat.idcontent;
            _context.SaveChanges();

            if (ModelState.IsValid)
            {
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(chat);
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

        // POST: Chat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Chat/")]
        /*Non web socket*/
        public async Task<IActionResult> CreateChatContentNonWs([Bind("id,Readed,Date,Uname,Text,idcontent,meadiapath,geo")] Chatcontent chatcontent)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(chatcontent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatExists(chatcontent.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok("");
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
