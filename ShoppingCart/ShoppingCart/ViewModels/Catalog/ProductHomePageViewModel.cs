using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Detail;
using ShoppingCart.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for home page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductHomePageViewModel : BaseViewModel
    {
        #region Constructor

        public ProductHomePageViewModel(IProductHomeDataService productHomeDataService,
            ICatalogDataService catalogDataService)
        {
            this.productHomeDataService = productHomeDataService;
            this.catalogDataService = catalogDataService;

            Device.BeginInvokeOnMainThread(() =>
            {
                FetchBannerImage();
                FetchOfferProducts();
                FetchRecentProducts();
            });

            //this.itemSelectedCommand = new DelegateCommand(this.ItemSelected);
        }

        #endregion

        #region Fields

        private ObservableCollection<Product> newArrivalProduts;

        private ObservableCollection<Product> offerProduts;

        private ObservableCollection<Product> recommendedProduts;

        private DelegateCommand itemSelectedCommand;

        private DelegateCommand viewAllCommand;

        private DelegateCommand masterPageOpenCommand;

        private string bannerImage;
        private readonly IProductHomeDataService productHomeDataService;
        private readonly ICatalogDataService catalogDataService;

        private bool isRecentProductExists;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with Xamarin.Forms Image, which displays the banner image.
        /// </summary>
        [DataMember(Name = "bannerimage")]
        public string BannerImage
        {
            get => App.BaseImageUrl + bannerImage;
            set
            {
                bannerImage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> NewArrivalProducts
        {
            get => newArrivalProduts;

            set
            {
                if (newArrivalProduts == value) return;

                newArrivalProduts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> OfferProducts
        {
            get => offerProduts;

            set
            {
                if (offerProduts == value) return;

                offerProduts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<Product> RecommendedProducts
        {
            get => recommendedProduts;

            set
            {
                if (recommendedProduts == value) return;

                recommendedProduts = value;
                OnPropertyChanged();
            }
        }

        public bool IsRecentProductExists
        {
            get => isRecentProductExists;
            set
            {
                isRecentProductExists = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public DelegateCommand ItemSelectedCommand =>
            itemSelectedCommand ?? (itemSelectedCommand = new DelegateCommand(ItemSelected));

        public DelegateCommand ViewAllCommand =>
            viewAllCommand ?? (viewAllCommand = new DelegateCommand(ViewAllProducts));

        public DelegateCommand MasterPageOpenCommand =>
            masterPageOpenCommand ?? (masterPageOpenCommand = new DelegateCommand(MasterPageOpened));

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to get the banner images
        /// </summary>
        public async void FetchBannerImage()
        {
            IsBusy = true;
            try
            {
                var banners = await productHomeDataService.GetBannersAsync();
                if (banners != null && banners.Count > 0) BannerImage = banners.FirstOrDefault().BannerImage;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the recent products
        /// </summary>
        public async void FetchRecentProducts()
        {
            try
            {
                if (App.CurrentUserId > 0)
                {
                    var recommends = await catalogDataService.GetRecentProductsAsync(App.CurrentUserId);
                    if (recommends != null && recommends.Count > 0)
                    {
                        IsRecentProductExists = true;
                        RecommendedProducts = new ObservableCollection<Product>(recommends);
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to get the offer product
        /// </summary>
        public async void FetchOfferProducts()
        {
            try
            {
                var offerProducts = await productHomeDataService.GetOfferProductsAsync();
                if (offerProducts != null && offerProducts.Count > 0)
                {
                    OfferProducts = new ObservableCollection<Product>(offerProducts);
                    NewArrivalProducts = new ObservableCollection<Product>(offerProducts);
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
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private async void MasterPageOpened(object attachedObject)
        {
            if (Application.Current.MainPage is NavigationPage &&
                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                ((Application.Current.MainPage as NavigationPage).CurrentPage as MasterDetailPage).IsPresented = true;
        }

        /// <summary>
        /// Invoked when an view all is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private void ViewAllProducts(object obj)
        {
            //Do something
        }

        #endregion
    }
}