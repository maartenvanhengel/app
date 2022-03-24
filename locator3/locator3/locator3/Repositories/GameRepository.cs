using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locator3
{
   public class GameRepository : IGameRepository<Game>
    {
        private readonly List<Game> games;
        List<Pointer> pointers = new List<Pointer>();
        
        public GameRepository()
        {
            Pointer pointer = new Pointer()
            { Latitude = 50.9478002257468,
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

            games = new List<Game>()
            {
                new Game{Id =0, name = "test", isPublic = true, endType="none", Pointers= null, coinsEanabled = true},
                new Game{Id =2, name = "public Item", isPublic = true, endType="none", Pointers= pointers, coinsEanabled = false},
                new Game{Id =1, name = "private Item", isPublic = true, endType="none", Pointers= pointers, coinsEanabled = true},
            };
        }
        public async Task<bool> AddItemAsync(Game game)
        {
            games.Add(game);

            return await Task.FromResult(true);
        }

        public async Task<Game> GetItemByIdAsync(int id)
        {
            return await Task.FromResult(games.FirstOrDefault(s => s.Id == id));
        }
        public async Task<IEnumerable<Game>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }

        public async Task<bool> UpdateItemAsync(Game game)
        {
            var oldItem = games.Where((Game arg) => arg.Id == game.Id).FirstOrDefault();
            games.Remove(oldItem);
            games.Add(game);

            return await Task.FromResult(true);
        }
    }

}
