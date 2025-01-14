﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using steadyforum.Server.Data;
using steadyforum.Server.Model;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using steadyforum.Server.Data;
using steadyforum.Server.Model;

namespace steadyforum.Server.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class NewsController : ControllerBase
    {
        private readonly steadyforumServerContext _context;

        public NewsController(steadyforumServerContext context)
        {
            _context = context;
        }

        // POST: Chat/List
        [HttpPost("News/{sessionid}/{chatname}/{passwordhash}")]
        public async Task<IActionResult> AddNews(string sessionid, [Bind("Id,Date,Title,Tag,Content,Mediapath,Reference,Like,Dislike,Repost")] News news)
        {
            if (_context.News == null)
            {
                return NotFound();
            }

            var user = await _context
                .User
                .Where(s => s.Sessionid == sessionid)
                .FirstOrDefaultAsync();
            if (user == null || user.Chatlist == null) { return BadRequest("{ \"status\" : \" expired session \"} "); }

            var pastValueChat = new News
            {
                Date = new DateOnly(),
                Title = news.Title,
                Content = news.Content,
                Reference = news.Reference,
                Mediapath = GenerateWallpaper(news.Content) /*yeah, its first bussines logic*/
            };

            if (ModelState.IsValid)
            {
                _context.Add(pastValueChat);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        /*https://vc.ru/midjourney/1145347-obzor-api-yes-ai-dlya-generacii-kartinok-s-pomoshyu-neiroseti-midjourney*/
        private string GenerateWallpaper(string content)
        {
            var mediapath = "";

            /*
            curl -X POST https://api.yesai.su/v2/midjourney/generations
            -H "Authorization: Bearer <token>"
            -H "Content-Type: application/json"
            -d '{
            "prompt": "текст вашего промпта",
            "style":3,
            "speed":"relax"
            }'

            */

            return mediapath;
        }
    }
}