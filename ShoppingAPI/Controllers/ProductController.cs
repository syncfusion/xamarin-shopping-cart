using System;
using System.Data.Entity;
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
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        /// <summary>
        /// Gets the prodcie by categoryId.
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetProductByCategoryId(int subCategoryId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var productEntity = new RepositoryDataAccessLayer<ProductEntity>())
                {
                    var products = productEntity.FindBy(a => a.SubCategoryId == subCategoryId && !a.IsDeleted)
                        .Include(a => a.User.UserWishlists).Include(a => a.PreviewImages).ToList();
                    if (products != null)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, products);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Gets the prodcu by Id.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetProductById(int productId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var productEntity = new RepositoryDataAccessLayer<ProductEntity>())
                {
                    var product = productEntity.FindBy(a => a.ID == productId && !a.IsDeleted).Include(s => s.Reviews)
                        .Include(w => w.Reviews.Select(q => q.ReviewImages)).Include(d => d.PreviewImages)
                        .Include(a => a.User.UserWishlists).FirstOrDefault();
                    product.Reviews.Where(s => !s.IsDeleted);

                    if (product != null)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, product);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Gets the reviews.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("reviews")]
        [HttpGet]
        public HttpResponseMessage GetReviews(int productId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var reviewEntity = new RepositoryDataAccessLayer<ReviewEntity>())
                {
                    var reviews = reviewEntity.FindBy(a => a.ProductId == productId && !a.IsDeleted)
                        .Include(a => a.ReviewImages).ToList();
                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK, reviews);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Adds the product to cart.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage AddCart(int userId, int productId)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<UserCartEntity>())
                {
                    var isProductAlreadyAdded = userCartEntity
                        .FindBy(a => a.UserId == userId && a.ProductId == productId && !a.IsDeleted).Any();
                    if (!isProductAlreadyAdded)
                    {
                        var userCart = new UserCartEntity
                        {
                            UserId = userId,
                            TotalQuantity = 1,
                            AddedDateTime = DateTime.Now,
                            IsDeleted = false,
                            ProductId = productId
                        };
                        userCartEntity.Add(userCart);
                        return response = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = true, Message = "Item added to cart successfully."});
                    }
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
        /// Gets the cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("carts")]
        [HttpGet]
        public HttpResponseMessage GetCartItems(int userId)
        {
            HttpResponseMessage httpResponse = null;
            var status = new Status();
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<UserCartEntity>())
                {
                    var userCart = userCartEntity.FindBy(a => a.UserId == userId && !a.IsDeleted)
                        .Include(s => s.Product).Include(q => q.Product.PreviewImages).ToList();
                    userCart.Where(s => !s.Product.IsDeleted).ToList();
                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK, userCart);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Adds the product to cart.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("myorders")]
        [HttpPost]
        public HttpResponseMessage AddOrderedItem(int userId, int productId)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<MyOrderEntity>())
                {
                    var isProductAlreadyAdded = userCartEntity
                        .FindBy(a => a.UserId == userId && a.ProductId == productId && !a.IsDeleted).Any();
                    if (!isProductAlreadyAdded)
                    {
                        var userCart = new MyOrderEntity()
                        {
                            UserId = userId,
                            TotalQuantity = 1,
                            AddedDateTime = DateTime.Now,
                            IsDeleted = false,
                            ProductId = productId
                        };
                        userCartEntity.Add(userCart);
                        return response = Request.CreateResponse(HttpStatusCode.OK,
                            new Status { IsSuccess = true, Message = "Item added to cart successfully." });
                    }
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK,
                    new Status { IsSuccess = false, Message = ex.Message });
            }

            return response;
        }

        /// <summary>
        /// Gets the ordered items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("myorders")]
        [HttpGet]
        public HttpResponseMessage GetOrderedItems(int userId)
        {
            HttpResponseMessage httpResponse = null;
            var status = new Status();
            try
            {
                using (var myOrderEntity = new RepositoryDataAccessLayer<MyOrderEntity>())
                {
                    var userCart = myOrderEntity.FindBy(a => a.UserId == userId && !a.IsDeleted)
                        .Include(s => s.Product).Include(q => q.Product.PreviewImages).ToList();
                    userCart.Where(s => !s.Product.IsDeleted).ToList();
                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK, userCart);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = false, ex.Message });
            }

            return httpResponse;
        }

        /// <summary>
        /// Updates the cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage UpdateCart(int userId, int productId, int quantity)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<UserCartEntity>())
                {
                    var userCart = userCartEntity
                        .FindBy(a => a.UserId == userId && a.ProductId == productId && !a.IsDeleted).FirstOrDefault();
                    if (userCart == null)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = false, Message = "Product is not found."});

                    userCart.TotalQuantity = quantity;
                    userCartEntity.Update(userCart);
                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK, new Status {IsSuccess = true});
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Removes the cart item.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public HttpResponseMessage RemoveCartItem(int userId, int productId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<UserCartEntity>())
                {
                    var userCart = userCartEntity
                        .FindBy(a => a.UserId == userId && a.ProductId == productId && !a.IsDeleted).FirstOrDefault();
                    if (userCart != null)
                    {
                        userCart.IsDeleted = true;
                        userCartEntity.Update(userCart);
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = true, Message = "Cart item successfully deleted."});
                    }

                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                        new Status {IsSuccess = false, Message = "Cart item is not deleted."});
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Removes the cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("cartitems/remove")]
        [HttpDelete]
        public HttpResponseMessage RemoveCartItems(int userId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userCartEntity = new RepositoryDataAccessLayer<UserCartEntity>())
                {
                    var userCart = userCartEntity.FindBy(a => a.UserId == userId && !a.IsDeleted).ToList();
                    if (userCart != null && userCart.Any())
                    {
                        foreach (var item in userCart)
                        {
                            item.IsDeleted = true;
                            userCartEntity.Update(item);
                        }

                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = true, Message = "Cart item successfully deleted."});
                    }

                    return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                        new Status {IsSuccess = false, Message = "Cart item is not deleted."});
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Get the payments.
        /// </summary>
        /// <returns></returns>
        [Route("payments")]
        [HttpGet]
        public HttpResponseMessage GetPayments()
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var paymentEntity = new RepositoryDataAccessLayer<PaymentEntity>())
                {
                    var paymentOptions = paymentEntity.GetAll().ToList();
                    if (paymentOptions != null && paymentOptions.Count > 0)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, paymentOptions);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK, new {IsSuccess = false, ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Gets the offer products.
        /// </summary>
        /// <returns></returns>
        [Route("offer")]
        [HttpGet]
        public HttpResponseMessage GetOfferProducts()
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var productEntity = new RepositoryDataAccessLayer<ProductEntity>())
                {
                    var offerProducts = productEntity.FindBy(a => a.DiscountPercent > 0 && !a.IsDeleted)
                        .Include(a => a.PreviewImages).ToList();
                    if (offerProducts != null && offerProducts.Count > 0)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, offerProducts);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Gets the recent products.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("recent")]
        [HttpGet]
        public HttpResponseMessage GetRecentProducts(int userId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var recentProductEntity = new RepositoryDataAccessLayer<RecentEntity>())
                {
                    var recentProducts = recentProductEntity.FindBy(a => a.UserId == userId).Include(b => b.Product)
                        .Select(s => s.Product);
                    var products = recentProducts.Where(a => !a.IsDeleted).Include(s => s.PreviewImages).ToList();
                    if (recentProducts != null && products.Count > 0)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, products);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Gets the banners.
        /// </summary>
        /// <returns></returns>
        [Route("banner")]
        [HttpGet]
        public HttpResponseMessage GetBanners()
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var bannerEntity = new RepositoryDataAccessLayer<BannerEntity>())
                {
                    var banners = bannerEntity.FindBy(a => !a.IsDeleted).ToList();
                    if (banners != null && banners.Count > 0)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, banners);
                }
            }
            catch (Exception ex)
            {
                httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return httpResponse;
        }

        /// <summary>
        /// Adds the recent product.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("addrecent")]
        [HttpPost]
        public HttpResponseMessage AddRecentProduct(int userId, int productId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var recentEntity = new RepositoryDataAccessLayer<RecentEntity>())
                {
                    var isItemAlreadyExists =
                        recentEntity.FindBy(a => a.UserId == userId && a.ProductId == productId).Any();
                    if (!isItemAlreadyExists)
                    {
                        var recent = new RecentEntity
                        {
                            UserId = userId,
                            ProductId = productId,
                            ViewedDate = DateTime.Now
                        };
                        recentEntity.Add(recent);
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, new Status {IsSuccess = true});
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