using System;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for usercard.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class UserCard
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string PaymentMode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedDate { get; set; }
    }
}