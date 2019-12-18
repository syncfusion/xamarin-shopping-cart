using System;
using System.IO;
using ShoppingCart.DataService;
using ShoppingCart.Droid.DependencyService;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidLocalStorage))]

namespace ShoppingCart.Droid.DependencyService
{
    public class AndroidLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "ShoppingKart.db3");
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}