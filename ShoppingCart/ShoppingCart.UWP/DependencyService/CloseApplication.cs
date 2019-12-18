using ShoppingCart.DependencyServices;
using ShoppingCart.UWP.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace ShoppingCart.UWP.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Application.Current.Quit();
        }
    }
}