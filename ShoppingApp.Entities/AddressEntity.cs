using System;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the AddressEntity.
    /// </summary>
    public class AddressEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the door number.
        /// </summary>
        public string DoorNo { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the user identity.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets whether IsDeleted enabled or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }
    }
}