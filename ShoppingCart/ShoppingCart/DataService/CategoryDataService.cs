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
    public class CategoryDataService : ICategoryDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="CategoryDataService" /> class.
        /// </summary>
        public CategoryDataService()
        {
            httpClient = new HttpClient();
        }

        #endregion

        #region Methods

        /// <summary>
        /// To get product categories.
        /// </summary>
        public async Task<List<Category>> GetCategories()
        {
            List<Category> Categories = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}category");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(result);
                        if (categories != null)
                            Categories = Mapper.Map<List<CategoryEntity>, List<Category>>(categories);
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

            return Categories;
        }

        /// <summary>
        /// This method is to get the subcategories using category id.
        /// </summary>
        public async Task<List<Category>> GetSubCategories(int categoryId)
        {
            List<Category> Categories = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}category?categoryId={categoryId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(result);
                        if (categories != null)
                            Categories = Mapper.Map<List<CategoryEntity>, List<Category>>(categories);
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

            return Categories;
        }

        #endregion
    }
}