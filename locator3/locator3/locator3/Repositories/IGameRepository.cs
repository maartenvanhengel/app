using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace locator3
{
   public interface IGameRepository<T>
    {
        Task<bool> AddItemAsync(T game);
        Task<bool> UpdateItemAsync(T game);
        Task<T> GetItemByIdAsync(int id);

        Task<IEnumerable<T>> GetItemsAsync(bool foreRefresh = false);
    }
}
