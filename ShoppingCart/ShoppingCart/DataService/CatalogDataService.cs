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
    /// Data service to load the data from database using Web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CatalogDataService : ICatalogDataService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates an instance for the <see cref="CatalogDataService" /> class.
        /// </summary>
        public CatalogDataService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// This method is to add the recent product.
        /// </summary>
        public async Task<Status> AddRecentProduct(int userId, int productId)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/addrecent?userId={userId}&productId={productId}");
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
        /// This method is to get the recenet/viewed products.
        /// </summary>
        public async Task<List<Product>> GetRecentProductsAsync(int userId)
        {
            var Products = new List<Product>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/recent?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var productsEntity = JsonConvert.DeserializeObject<List<ProductEntity>>(result);
                        if (productsEntity != null)
                            Products = Mapper.Map<List<ProductEntity>, List<Product>>(productsEntity);
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

        /// <summary>
        /// To get payment type options.
        /// </summary>
        public async Task<List<Payment>> GetPaymentOptionsAsync()
        {
            var Payments = new List<Payment>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/payments");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var paymentsEntity = JsonConvert.DeserializeObject<List<PaymentEntity>>(result);
                        if (paymentsEntity != null)
                            Payments = Mapper.Map<List<PaymentEntity>, List<Payment>>(paymentsEntity);
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

            return Payments;
        }

        /// <summary>
        /// This method is to get the product using id.
        /// </summary>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var Product = new Product();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products?productId={productId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var productEntity = JsonConvert.DeserializeObject<ProductEntity>(result);
                        if (productEntity != null) Product = Mapper.Map<ProductEntity, Product>(productEntity);
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

            return Product;
        }

        /// <summary>
        /// To get product using subcategory id.
        /// </summary>
        public async Task<List<Product>> GetProductBySubCategoryIdAsync(int subCategoryId)
        {
            var Products = new List<Product>();

            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products?subCategoryId={subCategoryId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var productsEntity = JsonConvert.DeserializeObject<List<ProductEntity>>(result);
                        if (productsEntity != null)
                            Products = Mapper.Map<List<ProductEntity>, List<Product>>(productsEntity);
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

        /// <summary>
        /// Get product reviews from database.
        /// </summary>
        public async Task<List<Review>> GetReviewsAsync(int productId)
        {
            var Reviews = new List<Review>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/reviews?productId={productId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var reviewsEntity = JsonConvert.DeserializeObject<List<ReviewEntity>>(result);
                        if (reviewsEntity != null)
                            Reviews = Mapper.Map<List<ReviewEntity>, List<Review>>(reviewsEntity);
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

            return Reviews;
        }
    }
}