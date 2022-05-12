using locator3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locator3.Repositories
{
   public class GameRepository : IGameRepository<Game>
    {
        private GameContext context;
        public GameRepository(GameContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddItemAsync(Game game)
        {
            await context.Games.AddAsync(game);
            return await SaveChangesAsync();
        }

        public async Task<Game> GetItemByIdAsync(string id)
        {
            return await context.Games.SingleAsync(item => item.Id == id);
            return null;
        }
        private async Task<bool> SaveChangesAsync()
        {
            int n = await context.SaveChangesAsync();

            if (n != 0)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Game>> GetItemsAsync(bool forceRefresh = false)
        {
            return await context.Games.ToListAsync();
        }

        Task<string> IGameRepository<Game>.AddItemAsync(Game game)
        {
            throw new NotImplementedException();
        }
    }

}
