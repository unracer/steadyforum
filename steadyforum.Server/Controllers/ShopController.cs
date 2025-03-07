﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using steadyforum.Server.Data;
using steadyforum.Server.Model;

namespace steadyforum.Server.Controllers
{
    [ApiController]
    /*[Route("api/[controller]")]*/
    [Route("/")]
    public class ShopController : ControllerBase
    {
        private readonly steadyforumServerContext _context;

        public ShopController(steadyforumServerContext context)
        {
            _context = context;
        }

        // GET: User/Details/5
        /*[HttpGet("Details/{sessionid}/{transactiontrackid}")]
        public async Task<IActionResult> Details(string? sessionid, string? transactiontrackid)
        {

            // after payment request track to payment system gatway

            if (sessionid == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Sessionid == sessionid);

            if (user == null || user.SessionCreate > user.SessionCreate.AddDays(1))
            {
                // 400
                return BadRequest("{ \"status\" : \"expired\"}");
            }

            // 200;
            return Ok("{ \"status\" : \"???\", \"days\" : \"" + "" +"\"}");
        }*/

        // POST: User/Login
        /*[HttpGet("Login/{name}/{passwordhash}")]
        public async Task<IActionResult> Login(string name, string passwordhash)
        {
            // double hash
            // dont know what for need salt couse every request have random hash of pass (cant compare)
            // byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            byte[] salt = {0};
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordhash!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));


            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            SHA256 mySHA256 = SHA256.Create();
            var sessionId = mySHA256.ComputeHash(stringChars.Select(c => (byte)c).ToArray());
            var encodesessionid = System.Convert.ToBase64String(sessionId).TrimEnd('=').Replace('+', '-').Replace('/', '_');

            var userfound = await _context
                .User
                .Where(m => m.Uname == name)
                .FirstOrDefaultAsync();

            if (userfound == null)
            {
                var valuePast = new User
                {
                    Uname = name,
                    Passwordhash = hashed,
                    Chatlist = "faq : keynone ", // exclude <space> from chatname/key in chatcreateController
                    Sessionid = encodesessionid,
                    SessionCreate = new DateTime()
                };

                if (ModelState.IsValid)
                {
                    _context.Add(valuePast);
                    await _context.SaveChangesAsync();
                    return Ok("{ \"status\" : \"created\", \"sessionid\" : \"" + encodesessionid + "\"}");
                }
            }
            else
            {
                if (userfound.Passwordhash != hashed)
                {
                    return Ok(userfound.id); 
                }

                var trackedUser = _context.User.Find(userfound.id);

                if (trackedUser == null) { return BadRequest("{ \"status\" : \"ep tvoy maty ya hui znat kak that may be\"}"); }

                trackedUser.Uname = userfound.Uname;
                trackedUser.Passwordhash = userfound.Passwordhash;
                trackedUser.Chatlist = userfound.Chatlist;
                trackedUser.Sessionid = encodesessionid;
                trackedUser.SessionCreate = new DateTime();

                _context.SaveChanges();

                return Ok("{ \"status\" : \"updated\", \"sessionid\" : \"" + encodesessionid + "\"}");
            }
            return BadRequest("{ \"status\" : \"fail\"}");
        }*/

        // GET: User/Create
        /* public IActionResult Create()
         {
             return Ok();
         }*/

        // POST: User/Order
        [HttpGet("Order/{SessionId}/{UnitId}/{externalLink}")]
        public async Task<IActionResult> Order(string SessionId, int? UnitId, string? ExternalLink)
        {

            /* strict recomend use 5post or alternate like watch dogs legion*/

            if (_context.User == null || _context.Chat == null)
            {
                return NotFound("null arg");
            }

            if (ExternalLink != null)
            {
                // dont pay only redirect couse we only search
                return Redirect(ExternalLink);
            }
            else if (UnitId != null)
            {
                var user = await _context
                .User
                .Where(s => s.Sessionid == SessionId)
                .FirstOrDefaultAsync();
                if (user == null) { return BadRequest(" expired session "); }

                var shopunit = await _context
                    .Shop
                    .Where(s => s.Unitid == UnitId)
                    .FirstOrDefaultAsync();
                    if (shopunit == null || shopunit.IsSale == false) { return BadRequest(" cant order "); }

                // 5post integration

            }
            return BadRequest("strict recomend use 5post");
        }

        [HttpGet("Search/{SessionId}/{wishline}")]
        public async Task<IActionResult> Search(string SessionId, string wishline)
        {

            /* strict recomend use 5post or alternate*/

            if (_context.User == null || _context.Chat == null)
            {
                return NotFound("null arg");
            }

            // search 

            var user = await _context
                .User
                .Where(s => s.Sessionid == SessionId)
                .FirstOrDefaultAsync();

            var response = "{" +
                "\"unit\":\"[]\"," +
                "\"predict\":\"[]\"," +
                "}";

            return Ok(response);
        }
        // GET: User/Edit/5
        /* public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var user = await _context.User.FindAsync(id);
             if (user == null)
             {
                 return NotFound();
             }
             return Ok(user);
         }*/

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,uname,passwordhash,chatlist,sessionid,sessionCreate")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
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
            return Ok(user);
        }*/

        // GET: User/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }*/

        // POST: User/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
