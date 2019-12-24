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
    public class CatalogDataService : ICatalogDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private CatalogDataService dataService;

        private readonly List<Product> recentProductItems = new List<Product>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "products")]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Populate the json file.
        /// </summary>
        public CatalogDataService DataService =>
            dataService = PopulateData<CatalogDataService>("ecommerce.json");

        #endregion

        #region Methods

        /// <summary>
        /// This method is to add the recent product.
        /// </summary>
        public async Task<Status> AddRecentProduct(int userId, int productId)
        {
            var status = new Status();
            try
            {
                if (!recentProductItems.Any(s => s.ID == productId))
                    recentProductItems.Add(new Product {ID = productId});

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
        /// This method is to get the recenet/viewed products.
        /// </summary>
        public async Task<List<Product>> GetRecentProductsAsync(int userId)
        {
            var recentProducts = new List<Product>();
            try
            {
                foreach (var recentProduct in recentProductItems)
                {
                    var item = DataService?.Products.FirstOrDefault(s => s.ID == recentProduct.ID);
                    recentProducts.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(recentProducts);
        }

        /// <summary>
        /// To get payment type options.
        /// </summary>
        public async Task<List<Payment>> GetPaymentOptionsAsync()
        {
            var Payments = new List<Payment>();
            try
            {
                Payments = new List<Payment>
                {
                    new Payment {PaymentMode = "Wells Fargo Bank Credit Card"},
                    new Payment {PaymentMode = "Debit / Credit Card"},
                    new Payment {PaymentMode = "NetBanking"},
                    new Payment {PaymentMode = "Cash on Delivery"},
                    new Payment {PaymentMode = "Wallet"}
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Payments);
        }

        /// <summary>
        /// This method is to get the product using id.
        /// </summary>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var Product = new Product();
            try
            {
                Product = DataService?.Products.FirstOrDefault(s => s.ID == productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Product);
        }

        /// <summary>
        /// To get product using subcategory id.
        /// </summary>
        public async Task<List<Product>> GetProductBySubCategoryIdAsync(int subCategoryId)
        {
            var Products = new List<Product>();

            try
            {
                Products = DataService?.Products.Where(s => s.SubCategoryId == subCategoryId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Products);
        }

        /// <summary>
        /// Get product reviews from database.
        /// </summary>
        public async Task<List<Review>> GetReviewsAsync(int productId)
        {
            var Reviews = new List<Review>();
            try
            {
                var product = DataService?.Products.FirstOrDefault(s => s.ID == productId);
                Reviews = product.Reviews;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Reviews);
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