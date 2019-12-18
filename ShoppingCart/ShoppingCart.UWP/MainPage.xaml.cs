namespace ShoppingCart.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new ShoppingCart.App());
        }
    }
}