using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for user.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int AddressId { get; set; }
    }
}