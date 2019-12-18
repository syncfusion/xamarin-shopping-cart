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
    /// Data service to load the data from json.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CartDataService : ICartDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private CartDataService dataService;

        private readonly List<UserCart> cartItems = new List<UserCart>();

        private readonly List<UserCart> orderedItems = new List<UserCart>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        /// <summary>
        /// To pouplate the json file.
        /// </summary>
        public CartDataService DataService =>
            dataService = PopulateData<CartDataService>("ecommerce.json");

        #endregion

        #region Methods

        /// <summary>
        /// To add the cart item.
        /// </summary>
        public async Task<Status> AddCartItemAsync(int userId, int productId)
        {
            var status = new Status();
            try
            {
                var isProductAlreadyAdded = cartItems.Any(s => s.ProductId == productId);
                if (!isProductAlreadyAdded)
                {
                    cartItems.Add(new UserCart
                    {
                        ProductId = productId, TotalQuantity = 1,
                        Product = DataService?.Products?.FirstOrDefault(s => s.ID == productId)
                    });
                    status.IsSuccess = true;
                }
                else
                {
                    status.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return await Task.FromResult(status);
        }

        /// <summary>
        /// Get cart item from database using web API.
        /// </summary>
        public async Task<List<UserCart>> GetCartItemAsync(int userId)
        {
            return await Task.FromResult(cartItems);
        }

        /// <summary>
        /// This method is to remove the cart item.
        /// </summary>
        public async Task<Status> RemoveCartItemAsync(int userId, int productId)
        {
            var status = new Status();
            try
            {
                var item = cartItems.FirstOrDefault(s => s.ProductId == productId);
                if (item != null) cartItems.Remove(item);

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
        /// This method is to remove the cart items.
        /// </summary>
        public async Task<Status> RemoveCartItemsAsync(int userId)
        {
            var status = new Status();
            try
            {
                foreach (var item in cartItems.ToList())
                    orderedItems.Add(item);

                foreach (var item in cartItems.ToList()) cartItems.Remove(item);

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
        /// To update the product quantity.
        /// </summary>
        public async Task<Status> UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            var status = new Status();
            try
            {
                cartItems.Where(s => s.ProductId == productId).Select(c =>
                {
                    c.TotalQuantity = quantity;
                    return c;
                });
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

        public async Task<List<UserCart>> GetOrderedItemsAsync(int userId)
        {
            return await Task.FromResult(orderedItems);
        }

        #endregion
    }
}