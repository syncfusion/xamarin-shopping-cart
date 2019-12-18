using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for pages with product.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class Product : INotifyPropertyChanged
    {
        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields

        private bool isFavourite;

        private string previewImage;

        private List<PreviewImage> previewImages;

        private int totalQuantity;

        #endregion

        #region Properties

        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "summary")] public string Summary { get; set; }

        [DataMember(Name = "description")] public string Description { get; set; }

        [DataMember(Name = "actualprice")] public double ActualPrice { get; set; }

        [DataMember(Name = "discountpercent")] public double DiscountPercent { get; set; }

        [DataMember(Name = "overallrating")] public double OverallRating { get; set; }

        [DataMember(Name = "totalquantity")]
        public int TotalQuantity
        {
            get => totalQuantity;
            set
            {
                totalQuantity = value;
                NotifyPropertyChanged("TotalQuantity");
            }
        }

        [DataMember(Name = "subcategoryid")] public int SubCategoryId { get; set; }

        [DataMember(Name = "reviews")] public List<Review> Reviews { get; set; }

        [DataMember(Name = "previewimages")]
        public List<PreviewImage> PreviewImages
        {
            get => previewImages;
            set
            {
                previewImages = value;
                NotifyPropertyChanged("PreviewImages");
            }
        }

        public string Category { get; set; }
        public double DiscountPrice => ActualPrice - ActualPrice * (DiscountPercent / 100);

        [DataMember(Name = "previewimage")]
        public string PreviewImage
        {
            get => App.BaseImageUrl + previewImage;
            set => previewImage = value;
        }

        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                isFavourite = value;
                NotifyPropertyChanged("IsFavourite");
            }
        }

        public string Seller { get; set; }

        private List<object> quantities;

        public List<object> Quantities
        {
            get => quantities;
            set
            {
                quantities = value;
                NotifyPropertyChanged("Quantities");
            }
        }

        public List<string> SizeVariants { get; set; }

        #endregion
    }
}