using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for review image.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class ReviewImage
    {
        #region

        private string image;

        #endregion

        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "reviewimage")]
        public string Image
        {
            get => App.BaseImageUrl + image;
            set => image = value;
        }
    }
}