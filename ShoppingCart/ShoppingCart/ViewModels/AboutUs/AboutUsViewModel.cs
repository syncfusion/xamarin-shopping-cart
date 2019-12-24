using ShoppingCart.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.AboutUs
{
    /// <summary>
    /// ViewModel of AboutUs templates.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AboutUsViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="AboutUsViewModel" /> class.
        /// </summary>
        public AboutUsViewModel()
        {
            productDescription =
                "Situated in the heart of Smith-town, Acme Products, Inc., has a long-standing tradition of selling the best products while providing the fastest service on the market. Since 1952, we’ve helped our customers identify their needs, understand their wants, and capture their dreams.";
            productIcon = "Icon.png";
            productVersion = "1.0";
        }

        #endregion

        #region Fields

        private string productDescription;

        private string productVersion;

        private string productIcon;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Properties        

        /// <summary>
        /// Gets or sets the description of a product or a company.
        /// </summary>
        /// <value>The product description.</value>
        public string ProductDescription
        {
            get => productDescription;

            set
            {
                productDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the product icon.
        /// </summary>
        /// <value>The product icon.</value>
        public string ProductIcon
        {
            get => productIcon;

            set
            {
                productIcon = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        /// <value>The product version.</value>
        public string ProductVersion
        {
            get => productVersion;

            set
            {
                productVersion = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}