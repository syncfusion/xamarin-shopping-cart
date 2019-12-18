using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShoppingAPI.Models;
using ShoppingAPI.Repositories;
using ShoppingApp.Entities;

namespace ShoppingAPI.Controllers
{
    /// <summary>
    /// This class is responsible for the category controller.
    /// </summary>
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var categoryEntity = new RepositoryDataAccessLayer<CategoryEntity>())
                {
                    var categories = categoryEntity.GetAll().ToList();

                    if (categories != null)
                        return response = Request.CreateResponse(HttpStatusCode.OK, categories);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return response;
        }

        /// <summary>
        /// Gets the subcategories.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetSubCategories(int categoryId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var subCategoryEntity = new RepositoryDataAccessLayer<SubCategoryEntity>())
                {
                    var subCategories = subCategoryEntity.FindBy(a => a.CategoryId == categoryId).ToList();

                    if (subCategories != null && subCategories.Count > 0)
                    {
                        var categories = new List<Category>();
                        foreach (var subCategory in subCategories)
                            categories.Add(new Category
                                {ID = subCategory.ID, Name = subCategory.Name, Icon = subCategory.Icon});
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, categories);
                    }
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return httpResponse;
        }
    }
}