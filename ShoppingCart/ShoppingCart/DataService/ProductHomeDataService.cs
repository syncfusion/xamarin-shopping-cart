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
    public class ProductHomeDataService : IProductHomeDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="ProductHomeDataService" /> class.
        /// </summary>
        public ProductHomeDataService()
        {
            httpClient = new HttpClient();
        }

        #endregion

        #region Methods

        /// <summary>
        /// To get the offer product from database.
        /// </summary>
        public async Task<List<Product>> GetOfferProductsAsync()
        {
            var Products = new List<Product>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/offer");
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
        /// To get the banner image.
        /// </summary>
        public async Task<List<Banner>> GetBannersAsync()
        {
            var Banners = new List<Banner>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}products/banner");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var bannersEntity = JsonConvert.DeserializeObject<List<BannerEntity>>(result);
                        if (bannersEntity != null)
                            Banners = Mapper.Map<List<BannerEntity>, List<Banner>>(bannersEntity);
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

            return Banners;
        }

        #endregion
    }
}