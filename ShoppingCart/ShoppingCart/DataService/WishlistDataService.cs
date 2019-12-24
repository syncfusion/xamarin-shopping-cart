using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ShoppingApp.Entities;
using ShoppingCart.Models;
using Xamarin.Forms.Internals;

namespace ShoppingCart.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class WishlistDataService : IWishlistDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="WishlistDataService" /> class.
        /// </summary>
        public WishlistDataService()
        {
            httpClient = new HttpClient();
        }

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
                var uri = new UriBuilder(
                    $"{App.BaseUri}users/wishlist?userId={userId}&productId={productId}&isFavorite={isFavorite}");
                var response = await httpClient.PostAsync(uri.ToString(), null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null) status = JsonConvert.DeserializeObject<Status>(result);
                }
            }
            catch (HttpRequestException ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return status;
        }

        /// <summary>
        /// To get wishlist items.
        /// </summary>
        public async Task<List<Product>> GetUserWishlistAsync(int userId)
        {
            var Products = new List<Product>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}users/wishlist?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var wishlistProducts = JsonConvert.DeserializeObject<List<ProductEntity>>(result);
                        if (wishlistProducts != null)
                            Products = Mapper.Map<List<ProductEntity>, List<Product>>(wishlistProducts);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Products;
        }

        #endregion
    }
}