using System;
using System.Collections.ObjectModel;
using System.Linq;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Bookmarks;
using ShoppingCart.Views.ErrorAndEmpty;
using ShoppingCart.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Detail
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetailPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DetailPageViewModel" /> class.
        /// </summary>
        public DetailPageViewModel(ICatalogDataService catalogDataService, ICartDataService cartDataService,
            IWishlistDataService wishlistDataService, Product selectedProduct)
        {
            this.catalogDataService = catalogDataService;
            this.cartDataService = cartDataService;
            this.wishlistDataService = wishlistDataService;
            this.selectedProduct = selectedProduct;

            Device.BeginInvokeOnMainThread(() =>
            {
                AddRecentProduct(selectedProduct.ID.ToString());
                FetchProduct(selectedProduct.ID.ToString());
                UpdateCartItemCount();
            });

            AddFavouriteCommand = new DelegateCommand(AddFavouriteClicked);
            NotificationCommand = new DelegateCommand(NotificationClicked);
            AddToCartCommand = new DelegateCommand(AddToCartClicked);
            LoadMoreCommand = new DelegateCommand(LoadMoreClicked);
            ShareCommand = new DelegateCommand(ShareClicked);
            VariantCommand = new DelegateCommand(VariantClicked);
            CardItemCommand = new DelegateCommand(CartClicked);
            BackButtonCommand = new DelegateCommand(BackButtonClicked);
        }

        #endregion

        #region Fields

        private double productRating;

        private Product productDetail;

        private readonly Product selectedProduct;

        private ObservableCollection<Category> categories;

        private bool isFavourite;

        private bool isReviewVisible;

        private int? cartItemCount;
        private readonly ICatalogDataService catalogDataService;
        private readonly ICartDataService cartDataService;
        private readonly IWishlistDataService wishlistDataService;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with SfRotator and labels, which display the product images and
        /// details.
        /// </summary>
        public Product ProductDetail
        {
            get => productDetail;

            set
            {
                if (productDetail == value) return;

                productDetail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get => categories;

            set
            {
                if (categories == value) return;

                categories = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the Favourite.
        /// </summary>
        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                isFavourite = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the empty message.
        /// </summary>
        public bool IsReviewVisible
        {
            get
            {
                if (productDetail != null && productDetail.Reviews != null)
                    if (productDetail.Reviews.Count == 0)
                        isReviewVisible = true;

                return isReviewVisible;
            }
            set
            {
                isReviewVisible = value;
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

        #region Commands

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public DelegateCommand AddFavouriteCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Notification button is clicked.
        /// </summary>
        public DelegateCommand NotificationCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public DelegateCommand AddToCartCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Show All button is clicked.
        /// </summary>
        public DelegateCommand LoadMoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Share button is clicked.
        /// </summary>
        public DelegateCommand ShareCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the size button is clicked.
        /// </summary>
        public DelegateCommand VariantCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when cart button is clicked.
        /// </summary>
        public DelegateCommand CardItemCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to add the recent product to the database.
        /// </summary>
        /// <param name="productId"></param>
        public async void AddRecentProduct(string productId)
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var status = await catalogDataService.AddRecentProduct(App.CurrentUserId, int.Parse(productId));
                    if (status != null && !status.IsSuccess)
                        await Application.Current.MainPage.DisplayAlert("Message", status.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the cart item count.
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
        /// This method is used to get the product based on product id.
        /// </summary>
        /// <param name="selectedProduct">Product id</param>
        public async void FetchProduct(string selectedProduct)
        {
            try
            {
                var product = await catalogDataService.GetProductByIdAsync(int.Parse(selectedProduct));
                if (product != null)
                {
                    ProductDetail = GetProductDetail(product);

                    var wishlistProducts = await wishlistDataService.GetUserWishlistAsync(App.CurrentUserId);
                    if (wishlistProducts != null && wishlistProducts.Count > 0)
                        ProductDetail.IsFavourite = wishlistProducts.Any(s => s.ID == ProductDetail.ID);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the product reviews and ratings.
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns></returns>
        public Product GetProductDetail(Product product)
        {
            var selectedPoductDetail = new Product();
            selectedPoductDetail = product;

            if (selectedPoductDetail.Reviews == null || selectedPoductDetail.Reviews.Count == 0)
                IsReviewVisible = true;
            else
                foreach (var review in selectedPoductDetail.Reviews)
                    productRating += review.Rating;

            if (productRating > 0)
                selectedPoductDetail.OverallRating = productRating / selectedPoductDetail.Reviews.Count;

            return selectedPoductDetail;
        }

        /// <summary>
        /// Invoked when the Favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddFavouriteClicked(object obj)
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    if (obj != null && obj is DetailPageViewModel model && model != null)
                    {
                        model.ProductDetail.IsFavourite = model.ProductDetail.IsFavourite ? false : true;
                        var status = await wishlistDataService.AddOrUpdateUserWishlist(App.CurrentUserId,
                            model.ProductDetail.ID, model.ProductDetail.IsFavourite);
                        if (status == null && !status.IsSuccess)
                            model.ProductDetail.IsFavourite = !model.ProductDetail.IsFavourite;
                        else if (status != null && status.IsSuccess)
                            selectedProduct.IsFavourite = ProductDetail.IsFavourite;
                    }
                }
                else
                {
                    var result = await Application.Current.MainPage.DisplayAlert("Message",
                        "Please login to add your favorite item.", "OK", "CANCEL");
                    if (result) Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when the Notification button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddToCartClicked(object obj)
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    if (obj != null && obj is DetailPageViewModel detailPageViewModel && detailPageViewModel != null)
                    {
                        var product = detailPageViewModel.ProductDetail;
                        var status = await cartDataService.AddCartItemAsync(App.CurrentUserId, product.ID);
                        if (status != null && status.IsSuccess)
                        {
                            CartItemCount++;
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
                        "Please login to add the product on your cart.", "OK", "CANCEL");
                    if (result) Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when Load more button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoadMoreClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the Share button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ShareClicked(object obj)
        {
            // Do something.
        }

        /// <summary>
        /// Invoked when the variant button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VariantClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private async void CartClicked(object obj)
        {
            if (CartItemCount > 0)
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            else
                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(false));
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