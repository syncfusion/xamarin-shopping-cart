using Android.App;
using ShoppingCart.DependencyServices;
using ShoppingCart.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]

namespace ShoppingCart.Droid.DependencyService
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            var activity = (Activity) Forms.Context;
            activity.FinishAffinity();
        }
    }
}