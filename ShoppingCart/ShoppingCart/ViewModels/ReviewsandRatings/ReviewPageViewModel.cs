using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.ReviewsandRatings
{
    /// <summary>
    /// ViewModel for review page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ReviewPageViewModel
    {
        #region Constructor

        public ReviewPageViewModel()
        {
            UploadCommand = new Command<object>(OnUploadTapped);
            SubmitCommand = new Command<object>(OnSubmitTapped);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the value for upload command.
        /// </summary>
        public Command<object> UploadCommand { get; set; }

        /// <summary>
        /// Gets or sets the value for submit command.
        /// </summary>
        public Command<object> SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Upload button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void OnUploadTapped(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void OnSubmitTapped(object obj)
        {
            Application.Current.MainPage.DisplayAlert("Success", "Your review has been successfully added", "", " ");
            Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}