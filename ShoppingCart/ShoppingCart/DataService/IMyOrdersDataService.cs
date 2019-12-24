using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface IMyOrdersDataService
    {
        Task<List<Product>> GetMyOrderslistAsync(int userId);
        Task<Status> AddOrUpdateOrderlist(int userId, int productId, bool isFavorite);
    }
}