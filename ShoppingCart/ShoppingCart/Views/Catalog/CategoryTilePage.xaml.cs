using ShoppingCart.DataService;
using ShoppingCart.ViewModels.Catalog;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.Catalog
{
    /// <summary>
    /// The Category Tile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTilePage" /> class.
        /// </summary>
        public CategoryTilePage(string selectedCategory)
        {
            InitializeComponent();
            var categoryDataService = App.MockDataService
                ? TypeLocator.Resolve<ICategoryDataService>()
                : DataService.TypeLocator.Resolve<ICategoryDataService>();
            BindingContext = new CategoryPageViewModel(categoryDataService, selectedCategory);
        }
    }
}