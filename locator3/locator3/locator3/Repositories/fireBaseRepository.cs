using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locator3.Repositories
{
    public class fireBaseRepository :IGameRepository<Game>
    {
        private const string BaseUrl = "https://mapsdata-b3812-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly ChildQuery _query;

        public fireBaseRepository()
        {
            string path = "Games";
            _query = new FirebaseClient(BaseUrl).Child(path);
        }

        public async Task<string> AddItemAsync(Game game)
        {
            try
            {
                var addedItem = await _query.PostAsync(game);
                game.Id = addedItem.Key;
                return game.Id;
            }
            catch
            {
                return "false";
            }
        }

        public async Task<Game> GetItemByIdAsync(string id)
        {
            try
            {
                var firebaseObjects = (await _query.OnceAsync<Game>()).Where(a => a.Key == id).FirstOrDefault();
                Game game = firebaseObjects.Object;
                return game;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Game>> GetItemsAsync(bool foreRefresh = false)
        {
            try
            {
                var firebaseObjects = await _query.OnceAsync<Game>();

                IEnumerable<Game> games = firebaseObjects
                    .Select(x => new Game
                    {
                        Id = x.Key,
                        name = x.Object.name,
                        isPublic = x.Object.isPublic,
                        coinsEanabled = x.Object.coinsEanabled,
                        endType = x.Object.endType
                    });
                return games;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(Game game)
        {
            try
            {
                Game copy = new Game() { name = game.name, coinsEanabled = game.coinsEanabled, endType = game.endType, isPublic = game.isPublic, Pointers = game.Pointers };
                await _query
                    .Child(game.Id)
                    .PutAsync(copy);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
