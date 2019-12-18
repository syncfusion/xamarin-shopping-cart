using ShoppingCart.ViewModels.ErrorAndEmpty;
using ShoppingCart.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ShoppingCart.Views.ErrorAndEmpty
{
    /// <summary>
    /// Page to show the empty cart
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmptyCartPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCartPage" /> class.
        /// </summary>
        public EmptyCartPage(bool isCartPage)
        {
            InitializeComponent();
            BindingContext =
                new EmptyCartPageViewModel(isCartPage, "CART IS EMPTY", "You don't have any items in your cart");
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Device.Idiom == TargetIdiom.Phone) ErrorImage.IsVisible = false;
            }
            else
            {
                ErrorImage.IsVisible = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new HomePage());
            return base.OnBackButtonPressed();
        }
    }
}