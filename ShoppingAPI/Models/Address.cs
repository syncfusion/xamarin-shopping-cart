using System;

namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or set the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or set the mobile no.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or set the door no.
        /// </summary>
        public string DoorNo { get; set; }

        /// <summary>
        /// Gets or set the area.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or set the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or set the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or set the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or set the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or set the user Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or set the deleted value.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or set the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or set the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}