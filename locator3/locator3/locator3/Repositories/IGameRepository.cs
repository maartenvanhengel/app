using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace locator3
{
   public interface IGameRepository<T>
    {
        Task<string> AddItemAsync(T game);
        Task<T> GetItemByIdAsync(string id);
        
        Task<IEnumerable<T>> GetItemsAsync(bool foreRefresh = false);
    }
}
