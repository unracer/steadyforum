using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using steadyforum.Server.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace steadyforum.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(steadyforumServerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.User.Any()) {
                return;   // DB has been seeded
            }

            var userlist = new User[] {
                new User{Uname="Carson",Passwordhash="Alexander",Chatlist="",Sessionid="",SessionCreate=(new DateTime())},
            };

            foreach (User s in userlist) {
                context.User.Add(s);
            }

            context.SaveChanges();

            var newslist = new News[] {
                new News{Date=(new DateOnly()),Title="Alexander",Tag="",Content="",Mediapath="",Reference="",Like=0,Dislike=0,Repost=0},
            };

            foreach (News s in newslist)
            {
                context.News.Add(s);
            }

            context.SaveChanges();

            var shoplist = new Shop[] {
                new Shop{Title="",Unitid=0,Datecreate=(new DateTime()),Mediapath="",Paymentsystem="",Externallink="",Description="",IsSale=false},
            };

            foreach (Shop s in shoplist)
            {
                context.Shop.Add(s);
            }

            context.SaveChanges();

            var chatlist = new Chat[] {
                new Chat{Idcontent=0,Chatname="FAQ",Passwordhash="",Userlist=""},
            };

            foreach (Chat s in chatlist)
            {
                context.Chat.Add(s);
            }

            context.SaveChanges();


        }
    }
}