using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using steadyforum.Server.Data;
using steadyforum.Server.Model;

namespace steadyforum.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly steadyforumServerContext _context;

        public UserController(steadyforumServerContext context)
        {
            _context = context;
        }

        // GET: User/Details/5
        [HttpGet("{sessionid}")]
        public async Task<IActionResult> Details(string? sessionid)
        {
            if (sessionid == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.sessionid == sessionid);

            if (user == null || user.sessionCreate > user.sessionCreate.AddDays(1))
            {
                // 400
                return BadRequest("{ \"status\" : \"expired\"}");
            }

            // 200;
            return Ok("{ \"status\" : \"valid\", \"days\" : " + (user.sessionCreate.AddDays(1))+"}");
        }

        // POST: User/Login
        [HttpGet("{name}/{passwordhash}")]
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
                .Where(m => m.uname == name)
                .FirstOrDefaultAsync();

            if (userfound == null)
            {
                var valuePast = new User
                {
                    uname = name,
                    passwordhash = hashed,
                    chatlist = "faq : keynone ", // exclude <space> from chatname/key in chatcreateController
                    sessionid = encodesessionid,
                    sessionCreate = new DateTime()
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
                if (userfound.passwordhash != hashed)
                {
                    return Ok(userfound.id); 
                }

                var trackedUser = _context.User.Find(userfound.id);

                if (trackedUser == null) { return BadRequest("{ \"status\" : \"ep tvoy maty ya hui znat kak that may be\"}"); }

                trackedUser.uname = userfound.uname;
                trackedUser.passwordhash = userfound.passwordhash;
                trackedUser.chatlist = userfound.chatlist;
                trackedUser.sessionid = encodesessionid;
                trackedUser.sessionCreate = new DateTime();

                _context.SaveChanges();

                return Ok("{ \"status\" : \"updated\", \"sessionid\" : \"" + encodesessionid + "\"}");
            }
            return BadRequest("{ \"status\" : \"fail\"}");
        }

        // GET: User/Create
        /* public IActionResult Create()
         {
             return Ok();
         }*/

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,uname,passwordhash,chatlist,sessionid,sessionCreate")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(user);
        }*/

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
            return _context.User.Any(e => e.id == id);
        }
    }
}
