using ShoppingCart.DataService;
using ShoppingCart.ViewModels.Transaction;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Transaction
{
    /// <summary>
    /// Page to show the Checkout details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutPage" /> class.
        /// </summary>
        public CheckoutPage()
        {
            InitializeComponent();
            var userDataService = App.MockDataService
                ? TypeLocator.Resolve<IUserDataService>()
                : DataService.TypeLocator.Resolve<IUserDataService>();
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            var catalogDataService = App.MockDataService
                ? TypeLocator.Resolve<ICatalogDataService>()
                : DataService.TypeLocator.Resolve<ICatalogDataService>();
            BindingContext = new CheckoutPageViewModel(userDataService, cartDataService, catalogDataService);
        }
    }
}