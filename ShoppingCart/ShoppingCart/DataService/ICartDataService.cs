using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface ICartDataService
    {
        Task<List<UserCart>> GetCartItemAsync(int userId);
        Task<Status> RemoveCartItemAsync(int userId, int productId);
        Task<Status> AddCartItemAsync(int userId, int productId);
        Task<Status> UpdateQuantityAsync(int userId, int productId, int quantity);
        Task<Status> RemoveCartItemsAsync(int userId);
        Task<List<UserCart>> GetOrderedItemsAsync(int userId);
    }
}