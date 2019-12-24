using ShoppingCart.DependencyServices;
using ShoppingCart.Models;
using ShoppingCart.Views.AboutUs;
using ShoppingCart.Views.ContactUs;
using ShoppingCart.Views.History;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace ShoppingCart.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            MasterPage.ListView.ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.ItemData as HomePageMasterMenuItem;
            if (item == null) return;

            Page page = null;

            if (item.Id == (int) MenuPage.Home)
            {
                page = new HomeTabbedPage();
                Detail = new NavigationPage(page);
            }
            else if (item.Id == (int) MenuPage.About)
            {
                page = new AboutUsSimplePage(){Title = item.Title};
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            else if (item.Id == (int) MenuPage.Contact)
            {
                page = new ContactUsPage() { Title = item.Title };
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                page = new HomeTabbedPage();
                Detail = new NavigationPage(page);
            }

            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Alert", "Are you want to close?", "Yes", "No"))
                {
                    base.OnBackButtonPressed();
                    var closeApplication = DependencyService.Get<ICloseApplication>();
                    if (closeApplication != null) closeApplication.CloseApp();
                }
            });

            return true;
        }
    }
}