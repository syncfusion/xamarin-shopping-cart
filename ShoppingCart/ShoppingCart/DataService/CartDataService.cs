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
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CartDataService : ICartDataService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates an instance for the <see cref="CartDataService" /> class.
        /// </summary>
        public CartDataService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// To add the cart item.
        /// </summary>
        public async Task<Status> AddCartItemAsync(int userId, int productId)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products?userId={userId}&productId={productId}");
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
        /// Get cart item from database using web API.
        /// </summary>
        public async Task<List<UserCart>> GetCartItemAsync(int userId)
        {
            var UserCarts = new List<UserCart>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/carts?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var userCartEntity = JsonConvert.DeserializeObject<List<UserCartEntity>>(result);
                        if (userCartEntity != null && userCartEntity.Count > 0)
                            UserCarts = Mapper.Map<List<UserCartEntity>, List<UserCart>>(userCartEntity);
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

            return UserCarts;
        }

        /// <summary>
        /// This method is to remove the cart item.
        /// </summary>
        public async Task<Status> RemoveCartItemAsync(int userId, int productId)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products?userId={userId}&productId={productId}");
                var response = await httpClient.DeleteAsync(uri.ToString());
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
        /// To update the product quantity.
        /// </summary>
        public async Task<Status> UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}products?userId={userId}&productId={productId}&quantity={quantity}");
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
        /// This method is to remove the cart item.
        /// </summary>
        public async Task<Status> RemoveCartItemsAsync(int userId)
        {
            var status = new Status();

            List<UserCart> orderedItems = await GetCartItemAsync(userId);
            foreach (UserCart item in orderedItems)
            {
                int productId = item.Product.ID;
                try
                {
                    var uri = new UriBuilder($"{App.BaseUri}products/myorders?userId={userId}&productId={productId}");
                    var response = await httpClient.PostAsync(uri.ToString(), null);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result != null) status = JsonConvert.DeserializeObject<Status>(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/cartitems/remove?userId={userId}");
                var response = await httpClient.DeleteAsync(uri.ToString());
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

        public async Task<List<UserCart>> GetOrderedItemsAsync(int userId)
        {
            var UserCarts = new List<UserCart>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/myorders?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var userCartEntity = JsonConvert.DeserializeObject<List<UserCartEntity>>(result);
                        if (userCartEntity != null && userCartEntity.Count > 0)
                            UserCarts = Mapper.Map<List<UserCartEntity>, List<UserCart>>(userCartEntity);
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

            return UserCarts;
        }
    }
}