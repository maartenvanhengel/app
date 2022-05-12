using locator3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace locator3.Repositories
{
    class SqlRepository: IGameRepository<Game>
    {
        private GamesContext context;

        public SqlRepository (GamesContext context)
        {
            this.context = context;
        }

        public Task<string> AddItemAsync(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetItemsAsync(bool forceRefresh = false)
        {
            return await context.Games.ToListAsync();
        }
    }
}
