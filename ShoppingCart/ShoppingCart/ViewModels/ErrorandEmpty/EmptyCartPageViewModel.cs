using ShoppingCart.Commands;
using ShoppingCart.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.ErrorAndEmpty
{
    /// <summary>
    /// ViewModel for empty cart page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class EmptyCartPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="EmptyCartPageViewModel" /> class.
        /// </summary>
        /// <param name="IsCartPage"></param>
        /// <param name="headerText"></param>
        /// <param name="contentText"></param>
        public EmptyCartPageViewModel(bool IsCartPage, string headerText, string contentText, string imagePath = "EmptyCart.svg")
        {
            isCartPage = IsCartPage;
            ImagePath = imagePath;
            Header = headerText;
            Content = contentText;
            GoBackCommand = new DelegateCommand(GoBack);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Go back button is clicked.
        /// </summary>
        public DelegateCommand GoBackCommand { get; set; }

        #endregion

        #region Methods        

        /// <summary>
        /// Invoked when the Go back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void GoBack(object obj)
        {
            if (!isCartPage)
                await Application.Current.MainPage.Navigation.PopAsync();
            else
                Application.Current.MainPage = new NavigationPage(new HomePage());
        }

        #endregion

        #region Fields

        private string imagePath;

        private string header;

        private string content;

        private readonly bool isCartPage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        public string ImagePath
        {
            get => imagePath;

            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get => header;

            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content
        {
            get => content;

            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}