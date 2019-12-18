using System.Threading;
using ShoppingCart.DependencyServices;
using ShoppingCart.iOS.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace ShoppingCart.iOS.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}