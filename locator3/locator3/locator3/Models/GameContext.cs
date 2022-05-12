using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.IO;

namespace locator3.Models
{
   public class GameContext : DbContext
    {
        private string connectionString;

        public GameContext(IFileHelper fileHelper)
        { 
        
            string databasePath = fileHelper.GetLocalFilePath("TodoSQLite2.db3");
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            /*
            List<Pointer> pointers = new List<Pointer>();

            Pointer pointer = new Pointer()
            {
                Latitude = 50.9478002257468,
                Longitude = 5.5187194344469805,
                Name = "yesNo Yes",
                type = "yesNo",
                text = "ja?;True"
            };
            pointers.Add(pointer);

            Pointer pointer2 = new Pointer();
            pointer2.Latitude = 50.889842019016896;
            pointer2.Longitude = 5.6286029214810425;
            pointer2.Name = "explanation";
            pointer2.type = "yesNo";
            pointer2.text = "hier woon ik?;False";
            pointers.Add(pointer2);

            Pointer pointer3 = new Pointer();
            pointer3.Latitude = 50.891479820408364;
            pointer3.Longitude = 5.633935152917103;
            pointer3.Name = "command";
            pointer3.text = "test dit";
            pointer3.type = "command";
            pointers.Add(pointer3);

            Pointer pointerGame1 = new Pointer() { Latitude = 50.92909223510005, Longitude = 5.394799257824568, Name = "battle1", type = "battle" };
            Pointer pointerGame2 = new Pointer() { Latitude = 50.889842019016896, Longitude = 5.6286029214810425, Name = "battle2", type = "battle" };
            Pointer pointerGame3 = new Pointer() { Latitude = 50.891479820408364, Longitude = 5.633935152917103, Name = "battle3", type = "battle" };
            Pointer pointerGame4 = new Pointer() { Latitude = 50.8831, Longitude = 5.6032, Name = "dorpstraat", type = "battle" };
            Pointer pointerGame5 = new Pointer() { Latitude = 50.9164, Longitude = 5.5920, Name = "stalkerweg", type = "battle" };

            List<Pointer> pointersGame = new List<Pointer>();
            pointersGame.Add(pointerGame1);
            pointersGame.Add(pointerGame2);
            pointersGame.Add(pointerGame3);
            pointersGame.Add(pointerGame4);
            pointersGame.Add(pointerGame5);


            Pointer pointer1Game0 = new Pointer() { Latitude = 50.9478002257468, Longitude = 5.5187194344469805, Name = "battle1", type = "battle" };
            List<Pointer> game0Pointers = new List<Pointer>();
            */
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = "0", name="iets", isPublic= true, coinsEanabled = false, endType = "none", Pointers = null}
             /*  new Game { Id = 0, name = "test", isPublic = true, endType = "none", Pointers = null, coinsEanabled = true },
                new Game { Id = 2, name = "public Item", isPublic = true, endType = "none", Pointers = pointers, coinsEanabled = false },
                new Game { Id = 1, name = "private Item", isPublic = false, endType = "none", Pointers = pointers, coinsEanabled = true },
                new Game { Id = 3, name = "dragon", isPublic = true, endType = "battle", Pointers = pointersGame, coinsEanabled = true },
                new Game { Id = 5, name = "dragon2", isPublic = true, endType = "battle", Pointers = game0Pointers, coinsEanabled = true }
          */  );
        }
    }
}
