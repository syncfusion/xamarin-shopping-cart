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
    public class ProductHomeDataService : IProductHomeDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private ProductHomeDataService dataService;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "products")]
        public List<Product> OfferProducts { get; set; }

        /// <summary>
        /// Image fetch from json.
        /// </summary>
        [DataMember(Name = "bannerimage")]
        public string BannerImage { get; set; }

        /// <summary>
        /// For populate the json file.
        /// </summary>
        public ProductHomeDataService DataService =>
            dataService = PopulateData<ProductHomeDataService>("ecommerce.json");

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
                Products = DataService?.OfferProducts.Where(s => s.DiscountPercent > 0).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Products);
        }

        /// <summary>
        /// To get the banner image.
        /// </summary>
        public async Task<List<Banner>> GetBannersAsync()
        {
            var Banners = new List<Banner>();
            try
            {
                Banners.Add(new Banner {BannerImage = DataService?.BannerImage});
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Banners);
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