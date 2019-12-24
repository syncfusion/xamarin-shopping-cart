using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private string email;

        private bool isInvalidEmail;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        public string Email
        {
            get => email;

            set
            {
                if (email == value) return;

                email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entered email is valid or invalid.
        /// </summary>
        public bool IsInvalidEmail
        {
            get => isInvalidEmail;

            set
            {
                if (isInvalidEmail == value) return;

                isInvalidEmail = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}