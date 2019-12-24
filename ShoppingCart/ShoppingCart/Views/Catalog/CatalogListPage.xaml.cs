using ShoppingCart.DataService;
using ShoppingCart.ViewModels.Catalog;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Catalog
{
    /// <summary>
    /// Page to show the catalog list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogListPage" /> class.
        /// </summary>
        public CatalogListPage(string selectedCategory)
        {
            InitializeComponent();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            var wishlistDataService = App.MockDataService
                ? TypeLocator.Resolve<IWishlistDataService>()
                : DataService.TypeLocator.Resolve<IWishlistDataService>();
            BindingContext = new CatalogPageViewModel(catalogDataService, cartDataService, wishlistDataService,
                selectedCategory);
        }
    }
}