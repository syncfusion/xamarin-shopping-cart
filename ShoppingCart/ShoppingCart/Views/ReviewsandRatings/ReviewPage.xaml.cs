using ShoppingCart.Models;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ShoppingCart.Views.ReviewsandRatings
{
    /// <summary>
    /// Page to get review from customer
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage
    {
        public ReviewPage(object obj)
        {
            InitializeComponent();
            productTitle.Text = (obj as UserCart).Product.Name;
            ProductImage.Source = (obj as UserCart).Product.PreviewImage;
        }
    }
}