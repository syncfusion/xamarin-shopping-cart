using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for category.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class Category
    {
        #region Fields

        private string icon;

        #endregion

        #region Properties

        [DataMember(Name = "id")] public int ID { get; set; }

        ///// <summary>
        ///// Gets or sets the property that has been bound with a label in SfExpander header, which displays the main category.
        ///// </summary>
        [DataMember(Name = "name")] public string Name { get; set; }

        ///// <summary>
        ///// Gets or sets the property that has been bound with an image, which displays the category.
        ///// </summary>
        [DataMember(Name = "icon")]
        public string Icon
        {
            get => App.BaseImageUrl + icon;
            set => icon = value;
        }

        [DataMember(Name = "subcategories")] public List<SubCategory> SubCategories { get; set; }

        #endregion
    }
}