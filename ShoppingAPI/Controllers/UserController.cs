using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShoppingAPI.Common;
using ShoppingAPI.Models;
using ShoppingAPI.Repositories;
using ShoppingAPI.Service;
using ShoppingApp.Entities;

namespace ShoppingAPI.Controllers
{
    /// <summary>
    /// This class is responsible for the user controller.
    /// </summary>
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly Random random;
        private readonly Random randomPassword;

        /// <summary>
        /// Initializes the user controller.
        /// </summary>
        public UserController()
        {
            random = new Random();
            randomPassword = new Random();
        }

        /// <summary>
        /// Gets the user address based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("address")]
        [HttpGet]
        public HttpResponseMessage GetUserAddress(int? userId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var addressEntity = new RepositoryDataAccessLayer<AddressEntity>())
                {
                    var address = addressEntity
                        .FindBy(a =>
                            userId == null && !a.IsDeleted || userId != null && a.UserId == userId && !a.IsDeleted)
                        .Include(s => s.User).ToList();
                    if (address != null)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, address);
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
        /// Login's the users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("authentication")]
        [HttpGet]
        public HttpResponseMessage Login(string email, string password)
        {
            var response = new HttpResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                    using (var userEntity = new RepositoryDataAccessLayer<UserEntity>())
                    {
                        var currentUser = userEntity
                            .FindBy(a => a.EmailId.Trim().ToLower() == email.Trim().ToLower() && !a.IsDeleted)
                            .FirstOrDefault();

                        if (currentUser == null)
                            return response = Request.CreateResponse(HttpStatusCode.OK,
                                new {IsSuccess = false, Message = "Invalid email id"});

                        var isValidUser = false;

                        if (!string.IsNullOrEmpty(currentUser.ForgotPassword))
                        {
                            isValidUser = CheckValidUser(password, currentUser.Salt, currentUser.ForgotPassword);
                            if (isValidUser)
                            {
                                currentUser.Password = currentUser.ForgotPassword;
                                currentUser.ForgotPassword = null;
                                userEntity.Update(currentUser);
                            }
                        }
                        else
                        {
                            isValidUser = CheckValidUser(password, currentUser.Salt, currentUser.Password);
                        }

                        if (isValidUser)
                            response = Request.CreateResponse(HttpStatusCode.OK,
                                new Status
                                {
                                    IsSuccess = true, Message = currentUser.ID.ToString(), UserName = currentUser.Name
                                });
                        else
                            response = Request.CreateResponse(HttpStatusCode.OK,
                                new Status {IsSuccess = false, Message = "Password is not valid"});
                    }
            }
            catch (Exception ex)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK,
                    new Status {IsSuccess = false, Message = ex.Message});
            }

            return response;
        }

        /// <summary>
        /// Signup's the  user accounts.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("signup")]
        [HttpPost]
        public HttpResponseMessage SignUp(User user)
        {
            var httpResponse = new HttpResponseMessage();

            try
            {
                using (var userEntity = new RepositoryDataAccessLayer<UserEntity>())
                {
                    var isEmailAlreadyRegistered = userEntity.FindBy(a =>
                        a.EmailId.Trim().ToLower() == user.EmailId.Trim().ToLower() && !a.IsDeleted).Any();
                    if (isEmailAlreadyRegistered)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = false, Message = "This email is already registered."});

                    if (user != null)
                    {
                        var newEntity = new UserEntity
                        {
                            Name = user.Name,
                            EmailId = user.EmailId,
                            Salt = GenerateSalt(8)
                        };
                        if (!string.IsNullOrEmpty(newEntity.Salt))
                            newEntity.Password = EncryptPassword(user.Password, newEntity.Salt);
                        newEntity.IsDeleted = false;
                        newEntity.CreatedDate = DateTime.Now;
                        newEntity.UpdatedDate = DateTime.Now;
                        userEntity.Add(newEntity);
                        httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = true, Message = "Signed up successfully"});
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

        /// <summary>
        /// Gets the user card.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("card")]
        [HttpGet]
        public HttpResponseMessage GetUserCard(int userId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userCardEntity = new RepositoryDataAccessLayer<UserCardEntity>())
                {
                    var userCards = userCardEntity.FindBy(a => a.UserId == userId && !a.IsDeleted || a.UserId == null)
                        .ToList();
                    if (userCards != null && userCards.Count > 0)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, userCards);
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
        /// Adds or updates the user wishlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="isFavorite"></param>
        /// <returns></returns>
        [Route("wishlist")]
        [HttpPost]
        public HttpResponseMessage AddOrUpdateUserWishlist(int userId, int productId, bool isFavorite)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userWishlistEntity = new RepositoryDataAccessLayer<UserWishlistEntity>())
                {
                    var userWishlist = userWishlistEntity.FindBy(a => a.UserId == userId && a.ProductId == productId)
                        .FirstOrDefault();
                    if (userWishlist != null)
                    {
                        userWishlist.IsFavorite = isFavorite;
                        userWishlistEntity.Update(userWishlist);
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK, new Status {IsSuccess = true});
                    }

                    var newProduct = new UserWishlistEntity
                    {
                        UserId = userId,
                        ProductId = productId,
                        IsFavorite = true
                    };
                    userWishlistEntity.Add(newProduct);
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
        /// Gets the user wishlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("wishlist")]
        [HttpGet]
        public HttpResponseMessage GetUserWishlist(int userId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userWishlistEntity = new RepositoryDataAccessLayer<UserWishlistEntity>())
                {
                    var userWishlistProduct = userWishlistEntity.FindBy(a => a.UserId == userId && a.IsFavorite)
                        .Include(s => s.Product).Select(e => e.Product);
                    var products = userWishlistProduct.Where(a => !a.IsDeleted).Include(s => s.PreviewImages).ToList();

                    if (products != null && products.Count > 0)
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
        /// Forgot password response.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [Route("forgot")]
        [HttpPost]
        public HttpResponseMessage ForgotPassword(string emailId)
        {
            var httpResponse = new HttpResponseMessage();
            try
            {
                using (var userEntity = new RepositoryDataAccessLayer<UserEntity>())
                {
                    var user = userEntity
                        .FindBy(a => a.EmailId.Trim().ToLower() == emailId.Trim().ToLower() && !a.IsDeleted)
                        .FirstOrDefault();

                    if (user == null)
                        return httpResponse = Request.CreateResponse(HttpStatusCode.OK,
                            new Status {IsSuccess = false, Message = "Invalid email id."});

                    user.ForgotPassword = EncryptPassword(GetTempPassword(), user.Salt);
                    user.UpdatedDate = DateTime.Now;
                    userEntity.Update(user);

                    var email = new EmailSender
                    {
                        RecipientName = $"{user.Name}",
                        To = user.EmailId
                    };
                    var newPassword = DecryptPassword(user.ForgotPassword, user.Salt);
                    email.SendForgotPasswordAsync(newPassword);

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
        /// Encrypts the password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        private string EncryptPassword(string password, string encryptKey)
        {
            return AuthenticationService.Encrypt(password, encryptKey);
        }

        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        private string DecryptPassword(string password, string encryptKey)
        {
            return AuthenticationService.Decrypt(password, encryptKey);
        }

        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        public string GenerateSalt(int keyLength)
        {
            var allowedUpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var allowedLowerCase = "abcdefghijklmnopqrstuvwxyz";
            var allowedNumeric = "1234567890";
            keyLength = random.Next(keyLength, 12);
            var k = 0;
            var chars = new char[keyLength];
            var position = new int[keyLength];
            //Positioning index were randomly generated 
            while (k < keyLength - 1)
            {
                var flag = false;
                var temp = random.Next(0, keyLength);
                int j;
                for (j = 0; j < keyLength; j++)
                    if (temp == position[j])
                    {
                        flag = true;
                        j = keyLength;
                    }

                if (!flag)
                {
                    position[k] = temp;
                    k++;
                }
            }

            //Random password was generate with combination of alphabet,numeric and special character
            for (var i = 0; i < keyLength; i++)
                chars[i] = i < 4
                    ? allowedLowerCase[random.Next(allowedLowerCase.Length)]
                    : i < 6
                        ? allowedNumeric[random.Next(allowedNumeric.Length)]
                        : allowedUpperCase[random.Next(allowedUpperCase.Length)];


            var sortedChar = new char[keyLength];
            for (var m = 0; m < keyLength; m++)
                sortedChar[m] = chars[position[m]];

            var ret = new string(sortedChar);
            return ret;
        }

        /// <summary>
        /// Gets the temporary password.
        /// </summary>
        /// <returns></returns>
        public string GetTempPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[randomPassword.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Checks the valid user.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="currentUserPassword"></param>
        /// <returns></returns>
        public bool CheckValidUser(string password, string salt, string currentUserPassword)
        {
            var encryptedPassword = EncryptPassword(password, salt);
            return string.Equals(encryptedPassword, currentUserPassword);
        }
    }
}