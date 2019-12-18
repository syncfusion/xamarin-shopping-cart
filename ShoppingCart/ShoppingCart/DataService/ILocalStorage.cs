using SQLite;

namespace ShoppingCart.DataService
{
    public interface ILocalStorage
    {
        SQLiteConnection GetConnection();
    }
}