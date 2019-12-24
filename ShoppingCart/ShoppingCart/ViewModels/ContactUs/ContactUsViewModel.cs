using System;
using System.Collections.ObjectModel;
using ShoppingCart.Commands;
using ShoppingCart.Models;
using ShoppingCart.Views.Home;
using Syncfusion.SfMaps.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.ContactUs
{
    /// <summary>
    /// ViewModel for contact us page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactUsViewModel : BaseViewModel
    {
        #region  Fields

        private DelegateCommand backButtonCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsViewModel" /> class.
        /// </summary>
        public ContactUsViewModel()
        {
            SendCommand = new DelegateCommand(Send);
            CustomMarkers = new ObservableCollection<MapMarker>();
            GetPinLocation();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public DelegateCommand SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Fields

        private ObservableCollection<MapMarker> customMarkers;

        private Point geoCoordinate;

        private string name;

        private string email;

        private string message;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CustomMarkers collection.
        /// </summary>
        public ObservableCollection<MapMarker> CustomMarkers
        {
            get => customMarkers;

            set
            {
                customMarkers = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the geo coordinate.
        /// </summary>
        public Point GeoCoordinate
        {
            get => geoCoordinate;

            set
            {
                geoCoordinate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods      

        /// <summary>
        /// Invoked when the send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void Send(object obj)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Message))
            {
                await Application.Current.MainPage.DisplayAlert("Message", "Your query sent successfully",
                    "Go to Home");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }

        /// <summary>
        /// This method is for getting the pin location detail.
        /// </summary>
        private void GetPinLocation()
        {
            CustomMarkers.Add(
                new LocationMarker
                {
                    PinImage = "Pin.png",
                    Header = "Sipes Inc",
                    Address = "7654 Cleveland street, Phoenixville, PA 19460",
                    EmailId = "dopuyi@hostguru.info",
                    PhoneNumber = "+1-202-555-0101",
                    CloseImage = "Close.png",
                    Latitude = "40.133808",
                    Longitude = "-75.516279"
                });

            foreach (var marker in CustomMarkers)
                GeoCoordinate = new Point(Convert.ToDouble(marker.Latitude), Convert.ToDouble(marker.Longitude));
        }
        
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