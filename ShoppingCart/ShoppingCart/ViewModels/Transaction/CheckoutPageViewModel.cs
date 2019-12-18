using System;
using System.Collections.ObjectModel;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Transaction;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Transaction
{
    /// <summary>
    /// ViewModel for Checkout page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CheckoutPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CheckoutPageViewModel" /> class.
        /// </summary>
        public CheckoutPageViewModel(IUserDataService userDataService, ICartDataService cartDataService,
            ICatalogDataService catalogDataService, IMyOrdersDataService myOrdersDataService = null)
        {
            this.userDataService = userDataService;
            this.cartDataService = cartDataService;
            this.catalogDataService = catalogDataService;
            DeliveryAddress = new ObservableCollection<Customer>();
            PaymentModes = new ObservableCollection<Payment>();

            Device.BeginInvokeOnMainThread(() =>
            {
                FetchAddresses();
                FetchPaymentOptions();
                FetchCartList();
            });

            EditCommand = new DelegateCommand(EditClicked);
            AddAddressCommand = new DelegateCommand(AddAddressClicked);
            PlaceOrderCommand = new DelegateCommand(PlaceOrderClicked);
            PaymentOptionCommand = new DelegateCommand(PaymentOptionClicked);
            ApplyCouponCommand = new DelegateCommand(ApplyCouponClicked);
        }

        #endregion

        #region Fields

        private ObservableCollection<Customer> deliveryAddress;

        private ObservableCollection<UserCart> orderedItems = new ObservableCollection<UserCart>();

        private ObservableCollection<Payment> paymentModes;

        private ObservableCollection<UserCart> cartDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;
        private readonly IUserDataService userDataService;
        private readonly ICartDataService cartDataService;
        private readonly ICatalogDataService catalogDataService;

        private DelegateCommand backButtonCommand;

        //IMyOrdersDataService myOrdersDataService;

        #endregion

        #region Public properties

       

        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the delivery address.
        /// </summary>
        public ObservableCollection<Customer> DeliveryAddress
        {
            get => deliveryAddress;

            set
            {
                if (deliveryAddress == value) return;

                deliveryAddress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the payment modes.
        /// </summary>
        public ObservableCollection<Payment> PaymentModes
        {
            get => paymentModes;

            set
            {
                if (paymentModes == value) return;

                paymentModes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<UserCart> OrderedItems
        {
            get => orderedItems;

            set
            {
                if (orderedItems == value) return;

                orderedItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<UserCart> CartDetails
        {
            get => cartDetails;

            set
            {
                if (cartDetails == value) return;

                cartDetails = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays total price.
        /// </summary>
        public double TotalPrice
        {
            get => totalPrice;

            set
            {
                if (totalPrice == value) return;

                totalPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays total discount price.
        /// </summary>
        public double DiscountPrice
        {
            get => discountPrice;

            set
            {
                if (discountPrice == value) return;

                discountPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays discount.
        /// </summary>
        public double DiscountPercent
        {
            get => discountPercent;

            set
            {
                if (discountPercent == value) return;

                discountPercent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Add new address button is clicked.
        /// </summary>
        public DelegateCommand AddAddressCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand PlaceOrderCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the payment option button is clicked.
        /// </summary>
        public DelegateCommand PaymentOptionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the apply coupon button is clicked.
        /// </summary>
        public DelegateCommand ApplyCouponCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand => backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));


        #endregion

        #region Methods

        /// <summary>
        /// This method is used to get the user address
        /// </summary>
        private async void FetchAddresses()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var addresses = await userDataService.GetAddresses();
                    if (addresses != null && addresses.Count > 0)
                        foreach (var address in addresses)
                            DeliveryAddress.Add(new Customer
                            {
                                CustomerId = address.UserId,
                                AddressType = address.AddressType,
                                CustomerName = address.Name,
                                MobileNumber = address.MobileNo,
                                Address = address.DoorNo + " " + address.Area + " " + address.City + " " +
                                          address.State + " " + address.Country + " " + address.PostalCode
                            });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the payment options and user card details
        /// </summary>
        private async void FetchPaymentOptions()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var userCards = await userDataService.GetUserCardsAsync(App.CurrentUserId);
                    if (userCards != null && userCards.Count > 0)
                        foreach (var userCard in userCards)
                            PaymentModes.Add(new Payment
                                {PaymentMode = userCard.PaymentMode, CardNumber = userCard.CardNumber});
                }

                var paymentOptions = await catalogDataService.GetPaymentOptionsAsync();
                if (paymentOptions != null)
                    foreach (var paymentOption in paymentOptions)
                        PaymentModes.Add(new Payment
                            {PaymentMode = paymentOption.PaymentMode, CardTypeIcon = paymentOption.CardTypeIcon});
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the cart products from database
        /// </summary>
        private async void FetchCartList()
        {
            try
            {
                var orderedData = await cartDataService.GetOrderedItemsAsync(App.CurrentUserId);
                if (orderedData != null && orderedData.Count > 0)
                    OrderedItems = new ObservableCollection<UserCart>(orderedData);


                var products = await cartDataService.GetCartItemAsync(App.CurrentUserId);
                if (products != null && products.Count > 0)
                {
                    CartDetails = new ObservableCollection<UserCart>(products);
                    UpdatePrice();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the product total price discount price and percentage
        /// </summary>
        private void UpdatePrice()
        {
            ResetPriceValue();

            if (CartDetails != null && CartDetails.Count > 0)
            {
                foreach (var cartDetail in CartDetails)
                {
                    if (cartDetail.TotalQuantity == 0) cartDetail.TotalQuantity = 1;

                    TotalPrice += cartDetail.Product.ActualPrice * cartDetail.TotalQuantity;
                    DiscountPrice += cartDetail.Product.DiscountPrice * cartDetail.TotalQuantity;
                    percent += cartDetail.Product.DiscountPercent;
                }

                DiscountPercent = percent > 0 ? percent / CartDetails.Count : 0;
            }
        }

        /// <summary>
        /// This method is used to reset the price amount.
        /// </summary>
        private void ResetPriceValue()
        {
            TotalPrice = 0;
            DiscountPercent = 0;
            DiscountPrice = 0;
            percent = 0;
        }

        /// <summary>
        /// Invoked when the Edit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void EditClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Add address button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddAddressClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Place order button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {
            if (CartDetails != null && CartDetails.Count > 0)
                foreach (var item in CartDetails)
                    OrderedItems.Add(item);

            var status = await cartDataService.RemoveCartItemsAsync(App.CurrentUserId);
            if (status != null && status.IsSuccess)
                await Application.Current.MainPage.Navigation.PushAsync(new PaymentSuccessPage());
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Invoked when the Payment option is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PaymentOptionClicked(object obj)
        {
            if (obj is RowDefinition rowDefinition && rowDefinition.Height.Value == 0)
                rowDefinition.Height = GridLength.Auto;
        }

        /// <summary>
        /// Invoked when the Apply coupon button is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ApplyCouponClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}