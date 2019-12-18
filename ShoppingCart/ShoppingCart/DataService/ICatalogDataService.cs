using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface ICatalogDataService
    {
        Task<List<Product>> GetProductBySubCategoryIdAsync(int subCategoryId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<List<Review>> GetReviewsAsync(int productId);
        Task<List<Payment>> GetPaymentOptionsAsync();
        Task<Status> AddRecentProduct(int userId, int productId);
        Task<List<Product>> GetRecentProductsAsync(int userId);
    }
}