using ShoppingCart.DataService;
using ShoppingCart.ViewModels.Bookmarks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Bookmarks
{
    /// <summary>
    /// Page to show the wishlist.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishlistPage : ContentPage
    {
        public WishlistPage()
        {
            InitializeComponent();
            var wishlistDataService = App.MockDataService
                ? TypeLocator.Resolve<IWishlistDataService>()
                : DataService.TypeLocator.Resolve<IWishlistDataService>();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            BindingContext = new WishlistViewModel(wishlistDataService, cartDataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var wishListVM = BindingContext as WishlistViewModel;
            if (wishListVM != null)
                Device.BeginInvokeOnMainThread(() => { wishListVM.FetchWishlist(); });
        }
    }
}