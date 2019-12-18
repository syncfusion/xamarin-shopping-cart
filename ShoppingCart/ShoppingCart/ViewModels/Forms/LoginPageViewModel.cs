using System;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            LoginCommand = new DelegateCommand(LoginClicked);
            SignUpCommand = new DelegateCommand(SignUpClicked);
            ForgotPasswordCommand = new DelegateCommand(ForgotPasswordClicked);
            SocialMediaLoginCommand = new DelegateCommand(SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
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

        #endregion

        #region Fields

        private string password;
        private readonly IUserDataService userDataService;

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

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public DelegateCommand ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public DelegateCommand SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !IsInvalidEmail)
                {
                    var response = await userDataService.Login(Email, Password);
                    if (response != null)
                    {
                        if (response.IsSuccess)
                        {
                            var userInfo = new UserInfo
                            {
                                UserId = int.Parse(response.Message),
                                EmailId = Email,
                                // userInfo.UserName = response.UserName;
                                UserName = "Jhon Deo",
                                IsNewUser = false
                            };
                            if (!App.MockDataService) SQLiteDatabase.Shared.InsertUserDetail(userInfo);

                            App.CurrentUserId = int.Parse(response.Message);
                            App.UserEmailId = Email;
                            Application.Current.MainPage = new NavigationPage(new HomePage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Message", response.Message, "OK");
                        }
                    }
                }
                else if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(password))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the email and password",
                        "OK");
                }
                else if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the email address", "OK");
                }
                else if (string.IsNullOrEmpty(password) && !IsInvalidEmail)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter the password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleSignUpPage());
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimpleForgotPasswordPage());
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}