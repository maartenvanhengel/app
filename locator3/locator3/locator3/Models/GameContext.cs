using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace locator3
{
    class GameContext : DbContext
    {
        private string connectionString;

        public GameContext(IFileHelper fileHelper)
        {
            string databasePath = fileHelper.GetLocalFilePath("TodoSQLite.db3");
            connectionString = $"Filename={databasePath}";

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }
        // public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {/* 
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Game>().HasData(
               new Game { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description = "This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description = "This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description = "This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description = "This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description = "This is an item description." }  
            );*/
        }
    }
}
