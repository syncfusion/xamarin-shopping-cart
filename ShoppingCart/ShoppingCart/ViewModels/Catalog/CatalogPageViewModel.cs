using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Bookmarks;
using ShoppingCart.Views.Detail;
using ShoppingCart.Views.ErrorAndEmpty;
using ShoppingCart.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CatalogPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CatalogPageViewModel" /> class.
        /// </summary>
        public CatalogPageViewModel(ICatalogDataService catalogDataService, ICartDataService cartDataService,
            IWishlistDataService wishlistDataService, string selectedCategory)
        {
            this.catalogDataService = catalogDataService;
            this.cartDataService = cartDataService;
            this.wishlistDataService = wishlistDataService;

            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateCartItemCount();
                FetchProducts(selectedCategory);
            });
        }

        #endregion

        #region Fields        

        private ObservableCollection<Product> products;

        private DelegateCommand addFavouriteCommand;

        private DelegateCommand itemSelectedCommand;

        public DelegateCommand cardItemCommand;

        private int cartItemCount;
        private readonly ICatalogDataService catalogDataService;
        private readonly ICartDataService cartDataService;
        private readonly IWishlistDataService wishlistDataService;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int CartItemCount
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
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ItemSelectedCommand =>
            itemSelectedCommand ?? (itemSelectedCommand = new DelegateCommand(ItemSelected));

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public DelegateCommand AddFavouriteCommand =>
            addFavouriteCommand ?? (addFavouriteCommand = new DelegateCommand(AddFavouriteClicked));

        /// <summary>
        /// Gets or sets the command will be executed when the cart icon button has been clicked.
        /// </summary>
        public DelegateCommand CardItemCommand =>
            cardItemCommand ?? (cardItemCommand = new DelegateCommand(CartClicked));

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to get the products based on category id
        /// </summary>
        /// <param name="selectedCategory"></param>
        public async void FetchProducts(string selectedCategory)
        {
            try
            {
                IsBusy = true;
                int subCategoryId;
                int.TryParse(selectedCategory, out subCategoryId);
                var products = await catalogDataService.GetProductBySubCategoryIdAsync(subCategoryId);
                if (products != null && products.Count > 0)
                {
                    Products = new ObservableCollection<Product>(products);

                    var wishlistProducts = await wishlistDataService.GetUserWishlistAsync(App.CurrentUserId);
                    if (wishlistProducts != null && wishlistProducts.Count > 0)
                        foreach (var product in Products.Where(a => wishlistProducts.Any(s => s.ID == a.ID)))
                            product.IsFavourite = true;
                }

                IsBusy = false;
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
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private async void ItemSelected(object attachedObject)
        {
            if (attachedObject != null && attachedObject is Product product && product != null)
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(product));
        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddFavouriteClicked(object obj)
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    if (obj != null && obj is Product product && product != null)
                    {
                        product.IsFavourite = product.IsFavourite ? false : true;
                        var isFavourite = product.IsFavourite;
                        var status =
                            await wishlistDataService.AddOrUpdateUserWishlist(App.CurrentUserId, product.ID,
                                isFavourite);
                        if (status == null || !status.IsSuccess) product.IsFavourite = !isFavourite;
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

        #endregion
    }
}