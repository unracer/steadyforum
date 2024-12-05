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

namespace steadyforum.Server.Data
{
    public class steadyforumServerContext : DbContext
    {
        public steadyforumServerContext (DbContextOptions<steadyforumServerContext> options)
            : base(options)
        {
            /*Database.EnsureDelete();*/
            /*Database.EnsureCreated();*/
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=steadyforumServerContext-53affff5-4b9a-4b21-bdc1-c3d17508cc0d;Trusted_Connection=True;MultipleActiveResultSets=true;");
            // cmd > dotnet ef migrations add InitialCreate
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<Chatcontent>().ToTable("Chatcontent");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Advertise>().ToTable("Advertise");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<Shop>().ToTable("Shop");
            modelBuilder.Entity<Statistic>().ToTable("Statistic");
        }

        public DbSet<steadyforum.Server.Model.Chat> Chat { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.Chatcontent> Chatcontent { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.User> User { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.Advertise> Advertise { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.News> News { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.Shop> Shop { get; set; } = default!;
        public DbSet<steadyforum.Server.Model.Statistic> Statistic { get; set; } = default!;
    }
}
