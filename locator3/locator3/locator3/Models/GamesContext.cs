using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace locator3.Models
{
    class GamesContext : DbContext
    {
        private string connectionString;

        public GamesContext(IFileHelper fileHelper)
        {
            string databasePath = fileHelper.GetLocalFilePath("TodoSQLite.db3");
            connectionString = $"Filename={databasePath}";

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}
