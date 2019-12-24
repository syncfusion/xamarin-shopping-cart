using System.Collections.ObjectModel;
using ShoppingCart.Models;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Home
{
    /// <summary>
    /// ViewModel for Home page master
    /// </summary>
    [Preserve(AllMembers = true)]
    public class HomePageMasterViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageMasterViewModel" /> class.
        /// </summary>
        public HomePageMasterViewModel()
        {
            UserEmail = App.UserEmailId;
            UserName = App.UserName;
            MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
            {
                new HomePageMasterMenuItem {Id = 2, Title = "About Us", TitleIcon = "\ue714"},
                new HomePageMasterMenuItem {Id = 3, Title = "Contact Us", TitleIcon = "\ue71c"}
            });
        }

        #endregion

        #region Private

        private string userName;

        private string userEmail;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the menu items
        /// </summary>
        public ObservableCollection<HomePageMasterMenuItem> MenuItems { get; set; }

        #endregion
    }
}