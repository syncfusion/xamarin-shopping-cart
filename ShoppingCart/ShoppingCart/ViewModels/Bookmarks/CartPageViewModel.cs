using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.ErrorAndEmpty;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Home;
using ShoppingCart.Views.Transaction;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Bookmarks
{
    /// <summary>
    /// ViewModel for cart page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CartPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CartPageViewModel" /> class.
        /// </summary>
        public CartPageViewModel(ICartDataService cartDataService)
        {
            this.cartDataService = cartDataService;
        }

        #endregion

        #region Fields

        private ObservableCollection<UserCart> userCarts;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private bool isEmptyViewVisible;

        private DelegateCommand placeOrderCommand;

        private Command removeCommand;

        private DelegateCommand quantitySelectedCommand;

        private DelegateCommand backButtonCommand;

        private readonly ICartDataService cartDataService;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<UserCart> UserCarts
        {
            get => userCarts;

            set
            {
                if (userCarts == value) return;

                userCarts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the total price.
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
        /// Gets or sets the property whether view is visible.
        /// </summary>
        public bool IsEmptyViewVisible
        {
            get => isEmptyViewVisible;

            set
            {
                if (isEmptyViewVisible == value) return;

                isEmptyViewVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays total discount price.
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
        /// Gets or sets the property that has been bound with label, which displays discount.
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

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays percent.
        /// </summary>
        public double Percent
        {
            get => percent;
            set
            {
                if (percent == value) return;
                percent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand PlaceOrderCommand =>
            placeOrderCommand ?? (placeOrderCommand = new DelegateCommand(PlaceOrderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command RemoveCommand => removeCommand ?? (removeCommand = new Command(RemoveClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the quantity is selected.
        /// </summary>
        public DelegateCommand QuantitySelectedCommand =>
            quantitySelectedCommand ?? (quantitySelectedCommand = new DelegateCommand(QuantitySelected));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods

        /// <summary>
        /// This method is for getting the cart items
        /// </summary>
        public async void FetchCartProducts()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var cartProducts = await cartDataService.GetCartItemAsync(App.CurrentUserId);
                    if (cartProducts != null && cartProducts.Count > 0)
                    {
                        IsEmptyViewVisible = false;
                        UserCarts = new ObservableCollection<UserCart>(cartProducts);
                        foreach (var cart in UserCarts) cart.Product.Quantities = new List<object> {"1", "2", "3"};
                        UpdatePrice();
                    }
                    else
                    {
                        if (Application.Current.MainPage is NavigationPage &&
                            (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                            IsEmptyViewVisible = true;
                        else
                            await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(true));
                    }
                }
                else
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Message",
                        "Please login to view your cart items", "OK", "CANCEL");
                    if (result) Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void RemoveClicked(object obj)
        {
            try
            {
                if (obj != null && obj is UserCart userCart && userCart != null)
                {
                    var status = await cartDataService.RemoveCartItemAsync(App.CurrentUserId, userCart.Product.ID);
                    if (status != null && status.IsSuccess)
                    {
                        UserCarts.Remove(userCart);
                        UpdatePrice();

                        if (userCarts.Count == 0)
                        {
                            if (Application.Current.MainPage is NavigationPage &&
                                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                                IsEmptyViewVisible = true;
                            else
                                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(true));
                        }
                        else
                        {
                            IsEmptyViewVisible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when the quantity is selected.
        /// </summary>
        /// <param name="selectedItem">The Object</param>
        private async void QuantitySelected(object selectedItem)
        {
            try
            {
                if (selectedItem != null && selectedItem is UserCart userCart && userCart != null)
                {
                    var status = await cartDataService.UpdateQuantityAsync(App.CurrentUserId, userCart.Product.ID,
                        userCart.TotalQuantity);
                    if (status != null && status.IsSuccess) UpdatePrice();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        public void UpdatePrice()
        {
            ResetPriceValue();

            if (UserCarts != null && UserCarts.Count > 0)
            {
                foreach (var cartDetail in UserCarts)
                {
                    if (cartDetail.TotalQuantity == 0) cartDetail.TotalQuantity = 1;

                    TotalPrice += cartDetail.Product.ActualPrice * cartDetail.TotalQuantity;
                    DiscountPrice += cartDetail.Product.DiscountPrice * cartDetail.TotalQuantity;
                    Percent += cartDetail.Product.DiscountPercent;
                }

                DiscountPercent = percent > 0 ? percent / UserCarts.Count : 0;
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
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            if (Application.Current.MainPage is NavigationPage &&
                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
            {
                var mainPage =
                    (((Application.Current.MainPage as NavigationPage).CurrentPage as MasterDetailPage)
                        .Detail as NavigationPage).CurrentPage as TabbedPage;
                mainPage.CurrentPage = mainPage.Children[0];
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        #endregion
    }
}