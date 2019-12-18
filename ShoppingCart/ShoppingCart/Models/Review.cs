using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for review list.
    /// </summary>
    [DataContract]
    [Preserve(AllMembers = true)]
    public class Review
    {
        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "profileimage")]
        public string ProfileImage
        {
            get => App.BaseImageUrl + profileImage;
            set => profileImage = value;
        }

        [DataMember(Name = "customername")] public string CustomerName { get; set; }

        [DataMember(Name = "revieweddate")]
        public string Date
        {
            get => reviewDate;
            set
            {
                reviewDate = value;
                if (!string.IsNullOrEmpty(reviewDate)) ReviewedDate = DateTime.Parse(reviewDate);
            }
        }

        public DateTime ReviewedDate { get; set; }

        public string StringDate { get; set; }

        [DataMember(Name = "rating")] public double Rating { get; set; }

        [DataMember(Name = "comment")] public string Comment { get; set; }

        [DataMember(Name = "reviewimages")] public List<ReviewImage> Images { get; set; }

        #region Field

        private string profileImage;

        private string reviewDate;

        #endregion
    }
}