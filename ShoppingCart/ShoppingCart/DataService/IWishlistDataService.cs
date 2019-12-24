using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface IWishlistDataService
    {
        Task<List<Product>> GetUserWishlistAsync(int userId);
        Task<Status> AddOrUpdateUserWishlist(int userId, int productId, bool isFavorite);
    }
}