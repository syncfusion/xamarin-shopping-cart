using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface IProductHomeDataService
    {
        Task<List<Product>> GetOfferProductsAsync();
        Task<List<Banner>> GetBannersAsync();
    }
}