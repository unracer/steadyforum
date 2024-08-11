using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using steadyforum.Server.Data;
using steadyforum.Server.Model;

namespace steadyforum.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
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
        [HttpGet("{sessionid}")]
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
                .Where(s => s.sessionid == sessionid)
                .FirstOrDefaultAsync();

            if (user == null) { return Problem(" expired session "); }

            return View(user.chatlist);
        }

        // POST: Chat/ContentOldApi
        [HttpGet("{sessionid}/{chatname}/{lastmessage}")]
        public async Task<IActionResult> ContentOldApi(string? sessionid, string? chatname, string? lastmessageAsString)
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
                .Where(s => s.sessionid == sessionid)
                .FirstOrDefaultAsync();

            var chat = await _context
                .Chat
                .Where(s => s.chatname == chatname)
                .FirstOrDefaultAsync();

            if ( user == null ) { return Problem(" expired session "); }
            if ( chat == null ) { return Problem(" denied chat or not exist ;) "); }

            if (!user.chatlist.Contains(chatname) || 
                !chat.userlist.Contains(user.uname)
                ) { return Problem(" denied, click <create chat> and login to chat again or call admin"); }

            // check chatname by regexp

            int lastmessage = int.Parse(lastmessageAsString);
            if (lastmessage == 0)
            {
                return (IActionResult)await _context
                    .Chat
                    .Where(s => s.chatname == chatname)
                    .ToListAsync();
            } else if (lastmessage > 0)
            {
                return (IActionResult)await _context
                    .Chat
                    .Where(s => s.chatname == chatname && s.id > lastmessage)
                    .ToListAsync();
            }
            
            return BadRequest("invalid arg");
        }

        // POST: Chat/Create
        [HttpGet("{sessionid}/{chatname}/{passwordhash}")]
        public async Task<IActionResult> LoginChat(string? sessionid, string? chatname,string? passwordhash)
        {

            // check for the all
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
                .Where(s => s.sessionid == sessionid)
                .FirstOrDefaultAsync();

            var chat = await _context
                .Chat
                .Where(s => s.chatname == chatname)
                .FirstOrDefaultAsync();

            if (user == null) { return Problem(" expired session "); }

            bool newlyCreated = false;
            if (chat == null) {
                // create

                var pastValueChat = new Chat
                {
                    Readed = new DateOnly(),
                    Date = new DateOnly(),
                    Uname = user.uname,
                    chatname = chatname,
                    passwordhash = passwordhash,
                    userlist = user.uname + " , ", // <space> for sec. bypass
                    idcontent = "",
                    message = "stay anon! - clear message every two message",
                };

                if (ModelState.IsValid)
                {
                    _context.Add(pastValueChat);
                    await _context.SaveChangesAsync();
                }

                // if success
                newlyCreated = true;
            }

            if (!newlyCreated && chat.passwordhash != passwordhash) { return Problem(" denied chat or not exist ;) "); }

            // if chat exist -> add access (add to user.chatlist and chat.userlist )
            var trackedUser = _context.User.Find(user.id);

            if (trackedUser == null) { return BadRequest("ep tvoy maty ya hui znat kak that may be"); }

            trackedUser.uname = user.uname;
            trackedUser.passwordhash = user.passwordhash;
            trackedUser.chatlist = user.chatlist + " , " + chatname + " : "+ passwordhash +" , "; // <space> for sec. bypass
            trackedUser.sessionid = user.sessionid;
            trackedUser.sessionCreate = user.sessionCreate;

            _context.SaveChanges();

            var trackedChat = _context.Chat.Find(chat.id);

            if (trackedUser == null) { return BadRequest("ep tvoy maty ya hui znat kak that may be"); }

            trackedChat.Readed = chat.Readed;
            trackedChat.Date = chat.Date;
            trackedChat.Uname = chat.Uname;
            trackedChat.chatname = chat.chatname;
            trackedChat.passwordhash = chat.passwordhash;
            trackedChat.userlist = chat.userlist + " , " + user.uname + " , "; // <space> for sec. bypass
            trackedChat.idcontent = chat.idcontent;
            trackedChat.message = chat.message;

            _context.SaveChanges();

            if (ModelState.IsValid)
            {
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chat);
        }

        // GET: Chat/Edit/5
        [HttpGet]
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
        }

        // POST: Chat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Readed,Date,Uname,chatname,passwordhash,userlist,idcontent,message")] Chat chat)
        {
            if (id != chat.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatExists(chat.id))
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
            return View(chat);
        }*/

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
