using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates an instance for the <see cref="UserDataService" /> class.
        /// </summary>
        public UserDataService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Login credential.
        /// </summary>
        public async Task<Status> Login(string email, string password)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}users/authentication?email={email}&password={password}");
                var response = await httpClient.GetAsync(uri.ToString());
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
        /// This method is to create a new account.
        /// </summary>
        public async Task<Status> SignUp(User user)
        {
            var status = new Status();
            try
            {
                var serializedUser = JsonConvert.SerializeObject(user);
                var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}users/signup");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
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
        /// This method is to get forgettable password.
        /// </summary>
        public async Task<Status> ForgotPassword(string emailId)
        {
            var status = new Status();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}users/forgot?emailId={emailId}");
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
        /// To get delivery address of logged user.
        /// </summary>
        public async Task<List<Address>> GetAddresses(int? userId)
        {
            var Addresses = new List<Address>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}users/address?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var userAddresses = JsonConvert.DeserializeObject<List<AddressEntity>>(result);
                        if (userAddresses != null)
                            Addresses = Mapper.Map<List<AddressEntity>, List<Address>>(userAddresses);
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

            return Addresses;
        }

        /// <summary>
        /// To get added card details.
        /// </summary>
        public async Task<List<UserCard>> GetUserCardsAsync(int userId)
        {
            var UserCards = new List<UserCard>();
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}users/card?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var userCards = JsonConvert.DeserializeObject<List<UserCardEntity>>(result);
                        if (userCards != null) UserCards = Mapper.Map<List<UserCardEntity>, List<UserCard>>(userCards);
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

            return UserCards;
        }
    }
}