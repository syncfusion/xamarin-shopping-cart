using System;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            LoginCommand = new DelegateCommand(LoginClicked);
            SignUpCommand = new DelegateCommand(SignUpClicked);
        }

        #endregion

        #region Fields

        private string name;

        private string password;

        private string confirmPassword;
        private readonly IUserDataService userDataService;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get => name;

            set
            {
                if (name == value) return;

                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get => password;

            set
            {
                if (password == value) return;

                password = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up
        /// page.
        /// </summary>
        public string ConfirmPassword
        {
            get => confirmPassword;

            set
            {
                if (confirmPassword == value) return;

                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public DelegateCommand SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleLoginPage());
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(ConfirmPassword))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please fill all the fields", "OK");
                }
                else if (!IsInvalidEmail)
                {
                    var isValidPassword = string.Equals(Password, ConfirmPassword);
                    if (isValidPassword)
                    {
                        var user = new User
                        {
                            Name = Name,
                            EmailId = Email,
                            Password = Password
                        };
                        var response = await userDataService.SignUp(user);

                        if (response.IsSuccess)
                            Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                        else
                            await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Password is mismatched", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        #endregion
    }
}