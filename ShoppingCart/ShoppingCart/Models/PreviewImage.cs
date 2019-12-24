using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for preview image.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class PreviewImage
    {
        #region Fields

        private string image;

        #endregion

        #region Properties

        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "previewimage")]
        public string Image
        {
            get => App.BaseImageUrl + image;
            set => image = value;
        }

        #endregion
    }
}