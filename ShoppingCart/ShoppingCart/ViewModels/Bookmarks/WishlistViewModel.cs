using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Bookmarks;
using ShoppingCart.Views.ErrorAndEmpty;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Bookmarks
{
    /// <summary>
    /// ViewModel for wishlist page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class WishlistViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="WishlistViewModel" /> class.
        /// </summary>
        public WishlistViewModel(IWishlistDataService wishlistDataService, ICartDataService cartDataService)
        {
            this.wishlistDataService = wishlistDataService;
            this.cartDataService = cartDataService;
        }

        #endregion

        #region Fields

        private ObservableCollection<Product> wishlistDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private bool isEmptyViewVisible;

        private int? cartItemCount;

        private DelegateCommand cardItemCommand;

        private Command addToCartCommand;

        private DelegateCommand deleteCommand;

        private DelegateCommand quantitySelectedCommand;
        private readonly IWishlistDataService wishlistDataService;
        private readonly ICartDataService cartDataService;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the wishlist details.
        /// </summary>
        public ObservableCollection<Product> WishlistDetails
        {
            get => wishlistDetails;

            set
            {
                if (wishlistDetails == value) return;

                wishlistDetails = value;
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
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int? CartItemCount
        {
            get => cartItemCount;

            set
            {
                cartItemCount = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command will be executed when the cart button has been clicked.
        /// </summary>
        public DelegateCommand CardItemCommand =>
            cardItemCommand ?? (cardItemCommand = new DelegateCommand(CartClicked));

        /// <summary>
        /// Gets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command AddToCartCommand => addToCartCommand ?? (addToCartCommand = new Command(AddToCartClicked));

        /// <summary>
        /// Gets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public DelegateCommand DeleteCommand => deleteCommand ?? (deleteCommand = new DelegateCommand(DeleteClicked));

        /// <summary>
        /// Gets the command that will be executed when the quantity is selected.
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
        /// This method is used to get the wish list items
        /// </summary>
        public async void FetchWishlist()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var wishlistProducts = await wishlistDataService.GetUserWishlistAsync(App.CurrentUserId);

                    if (wishlistProducts != null && wishlistProducts.Count > 0)
                    {
                        IsEmptyViewVisible = false;
                        WishlistDetails = new ObservableCollection<Product>(wishlistProducts);
                        foreach (var wishlist in WishlistDetails)
                            wishlist.Quantities = new List<object> {"1", "2", "3"};
                    }
                    else
                    {
                        IsEmptyViewVisible = true;
                    }
                }
                else
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Message",
                        "Please login to view your wishlist items", "OK", "CANCEL");
                    if (result) Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the cart item count
        /// </summary>
        public async void UpdateCartItemCount()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var cartItems = await cartDataService.GetCartItemAsync(App.CurrentUserId);
                    if (cartItems != null) CartItemCount = cartItems.Count;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CartClicked(object obj)
        {
            if (CartItemCount > 0)
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            else
                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(false));
        }

        /// <summary>
        /// Invoked when add to cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddToCartClicked(object obj)
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    if (obj != null && obj is Product product && product != null)
                    {
                        var status = await cartDataService.AddCartItemAsync(App.CurrentUserId, product.ID);
                        if (status != null && status.IsSuccess)
                        {
                            CartItemCount = CartItemCount + 1;
                            var result = await Application.Current.MainPage.DisplayAlert("Success",
                                $"The item {product.Name} has been added to cart", "Go to Cart", " ");
                            if (result) await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
                        }
                        else if (status != null && !status.IsSuccess)
                        {
                            var result = await Application.Current.MainPage.DisplayAlert("Alert",
                                "This item has been already added in cart", "Go to Cart", " ");
                            if (result) await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
                        }
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
        /// Invoked when an delete button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void DeleteClicked(object obj)
        {
            try
            {
                if (obj != null && obj is Product product && WishlistDetails.Count > 0)
                {
                    WishlistDetails.Remove(product);
                    await wishlistDataService.AddOrUpdateUserWishlist(App.CurrentUserId, product.ID, false);
                    if (WishlistDetails.Count == 0)
                        IsEmptyViewVisible = true;
                    else if (IsEmptyViewVisible) IsEmptyViewVisible = false;
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
        private void QuantitySelected(object selectedItem)
        {
            UpdatePrice();
        }

        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        private void UpdatePrice()
        {
            ResetPriceValue();

            if (WishlistDetails != null && WishlistDetails.Count > 0)
            {
                foreach (var wishlistDetail in WishlistDetails)
                {
                    if (wishlistDetail.TotalQuantity == 0) wishlistDetail.TotalQuantity = 1;

                    TotalPrice += wishlistDetail.ActualPrice * wishlistDetail.TotalQuantity;
                    DiscountPrice += wishlistDetail.DiscountPrice * wishlistDetail.TotalQuantity;
                    percent += wishlistDetail.DiscountPercent;
                }

                DiscountPercent = percent > 0 ? percent / WishlistDetails.Count : 0;
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