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

            Pointer pointerGame1 = new Pointer() { Latitude = 50.9478002257468, Longitude = 5.5187194344469805, Name = "battle1", type = "battle" };
            Pointer pointerGame2 = new Pointer() { Latitude = 50.889842019016896, Longitude = 5.6286029214810425, Name = "battle2", type = "battle" };
            Pointer pointerGame3 = new Pointer() { Latitude = 50.891479820408364, Longitude = 5.633935152917103, Name = "battle3", type = "battle" };
            List<Pointer> pointersGame = new List<Pointer>();
            pointersGame.Add(pointerGame1);
            pointersGame.Add(pointerGame2);
            pointersGame.Add(pointerGame3);


            Pointer pointerGent1 = new Pointer() { Latitude = 51.05548239216998, Longitude = 3.726165125510954, Name = "parking", type = "goTo" };
            Pointer pointerGent2 = new Pointer() { Latitude = 51.054749161133365, Longitude = 3.7254639834292766, Name = "hotel", type = "explanation" , text="slaap lekker" };
            Pointer pointerGent3 = new Pointer() { Latitude = 51.04862569023387, Longitude = 3.7287017751051965, Name = "Fietsen", type = "command", text = "haal de fietsen op" };
            Pointer pointerGent4 = new Pointer() { Latitude = 51.053872969749825, Longitude = 3.720349593441716, Name = "Sint-Michielsplein", type = "goTo" };
            Pointer pointerGent5 = new Pointer() { Latitude = 51.057581286335086, Longitude = 3.7208950118064266, Name = "Gravensteen", type = "yesNo", text = "Vindt je het mooi?;True" };
            Pointer pointerGent6 = new Pointer() { Latitude = 51.05180632607743, Longitude = 3.7288604707294177, Name = "Duivelsteen", type = "explanation", text = "Het Geeraard de Duivelsteen is een gebouw in de Belgische stad Gent. Het steen speelde een belangrijke rol bij de verdediging van de Portus aan de Reep, een handelsnederzetting die aan de wieg stond van het ontstaan van Gent." };
            Pointer pointerGent7 = new Pointer() { Latitude = 51.05468952230646, Longitude = 3.721801331274747, Name = "korenmarkt", type = "yesNo", text = "Is dit het mooiste?;True" };
            List<Pointer> pointersGent = new List<Pointer>();
            pointersGent.Add(pointerGent1);
            pointersGent.Add(pointerGent2);
            pointersGent.Add(pointerGent3);
            pointersGent.Add(pointerGent4);
            pointersGent.Add(pointerGent5);
            pointersGent.Add(pointerGent6);
            pointersGent.Add(pointerGent7);
            games = new List<Game>()
            {
                new Game{Id =0, name = "test", isPublic = true, endType="none", Pointers= null, coinsEanabled = true},
                new Game{Id =2, name = "public Item", isPublic = true, endType="none", Pointers= pointers, coinsEanabled = false},
                new Game{Id =1, name = "private Item", isPublic = false, endType="none", Pointers= pointers, coinsEanabled = true},
                new Game{Id =3, name = "dragon", isPublic = true, endType="battle", Pointers= pointersGame, coinsEanabled = true},
                new Game{Id =4, name = "Gent", isPublic = true, endType="none", Pointers= pointersGent, coinsEanabled = false},
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
