using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using Xamarin.Forms.Internals;

namespace ShoppingCart.MockDataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class WishlistDataService : IWishlistDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private WishlistDataService dataService;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Displays the collection of wishlist items.
        /// </summary>
        public List<Product> WishListItems { get; set; }

        /// <summary>
        /// For populate the json file.
        /// </summary>
        public WishlistDataService DataService =>
            dataService = PopulateData<WishlistDataService>("ecommerce.json");

        #endregion

        #region Methods

        /// <summary>
        /// To add or update the user wishlist.
        /// </summary>
        public async Task<Status> AddOrUpdateUserWishlist(int userId, int productId, bool isFavorite)
        {
            var status = new Status();
            try
            {
                if (WishListItems == null) WishListItems = new List<Product>();

                if (WishListItems.Any(s => s.ID == productId && s.IsFavourite))
                    WishListItems.Where(s => s.ID == productId).Select(s =>
                    {
                        s.IsFavourite = isFavorite;
                        return s;
                    }).ToList();
                else
                    WishListItems.Add(new Product {ID = productId, IsFavourite = isFavorite});

                status.IsSuccess = true;
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return await Task.FromResult(status);
        }

        /// <summary>
        /// To get wishlist items.
        /// </summary>
        public async Task<List<Product>> GetUserWishlistAsync(int userId)
        {
            var favouriteItems = new List<Product>();
            try
            {
                if (WishListItems != null)
                    foreach (var item in WishListItems)
                        if (item.IsFavourite)
                        {
                            var favouriteItem = DataService?.Products.FirstOrDefault(s => s.ID == item.ID);
                            favouriteItems.Add(favouriteItem);
                        }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(favouriteItems);
        }

        /// <summary>
        /// Populates the data from json file.
        /// </summary>
        /// <param name="fileName">Json file to fetch data.</param>
        private static T PopulateData<T>(string fileName)
        {
            var file = "ShoppingCart.MockData." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T) serializer.ReadObject(stream);
            }

            return obj;
        }

        #endregion
    }
}