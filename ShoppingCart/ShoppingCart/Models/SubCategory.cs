using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for subcategory.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class SubCategory
    {
        #region Fields

        #endregion

        #region Properties

        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "icon")] public string Icon { get; set; }

        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "categoryid")] public int CategoryId { get; set; }

        #endregion
    }
}