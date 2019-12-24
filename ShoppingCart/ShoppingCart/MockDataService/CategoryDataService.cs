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
    public class CategoryDataService : ICategoryDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private CategoryDataService dataService;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "categories")]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// For populate the json file.
        /// </summary>
        public CategoryDataService DataService =>
            dataService = PopulateData<CategoryDataService>("ecommerce.json");

        #endregion

        #region Methods

        /// <summary>
        /// To get product categories.
        /// </summary>
        public async Task<List<Category>> GetCategories()
        {
            return await Task.FromResult(DataService?.Categories);
        }

        /// <summary>
        /// This method is to get the subcategories using category id.
        /// </summary>
        public async Task<List<Category>> GetSubCategories(int categoryId)
        {
            var Categories = new List<Category>();
            try
            {
                foreach (var category in DataService?.Categories)
                {
                    var subCategories = category?.SubCategories?.Where(x => x.CategoryId == categoryId).ToList();
                    if (subCategories != null && subCategories.Count > 0)
                        foreach (var subCategory in subCategories)
                            Categories.Add(new Category
                                {ID = subCategory.ID, Name = subCategory.Name, Icon = subCategory.Icon});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Categories);
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