using ShoppingCart.DataService;
using ShoppingCart.ViewModels.Catalog;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Catalog
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductHomePage : ContentPage
    {
        public ProductHomePage()
        {
            InitializeComponent();
            var productHomeDataService = App.MockDataService
                ? TypeLocator.Resolve<IProductHomeDataService>()
                : DataService.TypeLocator.Resolve<IProductHomeDataService>();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            BindingContext = new ProductHomePageViewModel(productHomeDataService, catalogDataService);
        }
    }
}