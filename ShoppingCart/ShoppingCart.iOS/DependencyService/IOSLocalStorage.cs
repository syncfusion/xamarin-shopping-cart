using System;
using System.IO;
using ShoppingCart.DataService;
using ShoppingCart.iOS.DependencyService;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSLocalStorage))]

namespace ShoppingCart.iOS.DependencyService
{
    public class IOSLocalStorage : ILocalStorage
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "ShoppingKart.db";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}