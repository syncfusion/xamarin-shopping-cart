using System.Collections.ObjectModel;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.ViewModels.History;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TypeLocator = ShoppingCart.MockDataService.TypeLocator;

namespace ShoppingCart.Views.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrdersPage : ContentPage
    {
        public MyOrdersPage()
        {
            InitializeComponent();
            FetchData();
        }

        public ObservableCollection<UserCart> Data { get; set; }

        private async void FetchData()
        {
            var cartDataService = App.MockDataService
                ? TypeLocator.Resolve<ICartDataService>()
                : DataService.TypeLocator.Resolve<ICartDataService>();
            var orderedItem = await cartDataService.GetOrderedItemsAsync(App.CurrentUserId);
            if (orderedItem != null && orderedItem.Count > 0)
                (BindingContext as MyOrdersPageViewModel).MyOrders = new ObservableCollection<UserCart>(orderedItem);
        }
    }
}