using System.IO;
using Windows.Storage;
using ShoppingCart.DataService;
using ShoppingCart.UWP.DependencyService;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(WindowsLocalStorage))]

namespace ShoppingCart.UWP.DependencyService
{
    public class WindowsLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "ShoppingKart.db";
            var documentPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}