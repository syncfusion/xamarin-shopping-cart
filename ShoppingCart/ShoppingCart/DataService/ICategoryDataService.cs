using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.DataService
{
    public interface ICategoryDataService
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetSubCategories(int categoryId);
    }
}