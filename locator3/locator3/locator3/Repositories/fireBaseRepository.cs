using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locator3.Repositories
{
    public class fireBaseRepository :IGameRepository<Game>
    {
        private const string BaseUrl = "https://mapsdata-b3812-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly ChildQuery _query;

        private fireBaseRepository()
        {
            string path = "games";
            _query = new FirebaseClient(BaseUrl).Child(path);
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
                return null;
            }
        }
    }
}
