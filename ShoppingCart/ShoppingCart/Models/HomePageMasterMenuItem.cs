using System;
using System.ComponentModel;

namespace ShoppingCart.Models
{
    public class HomePageMasterMenuItem
    {
        public HomePageMasterMenuItem()
        {
            TargetType = typeof(HomePageMasterMenuItem);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string TitleIcon { get; set; }
    }

    public enum MenuPage
    {
        [Description("home")] Home = 1,
        [Description("about us")] About = 2,
        [Description("contact us")] Contact = 3,
    }
}