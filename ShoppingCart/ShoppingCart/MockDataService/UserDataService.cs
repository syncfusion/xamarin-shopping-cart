using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using Xamarin.Forms.Internals;

namespace ShoppingCart.MockDataService
{
    /// <summary>
    /// Data service to load the data from database using Web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class UserDataService : IUserDataService
    {
        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Login credential.
        /// </summary>
        public async Task<Status> Login(string email, string password)
        {
            var status = new Status();
            try
            {
                status.IsSuccess = true;
                status.Message = "1";
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return await Task.FromResult(status);
        }

        /// <summary>
        /// This method is to create a new account.
        /// </summary>
        public async Task<Status> SignUp(User user)
        {
            var status = new Status();
            try
            {
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
        /// This method is to get forgettable password.
        /// </summary>
        public async Task<Status> ForgotPassword(string emailId)
        {
            var status = new Status();
            try
            {
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
        /// To get delivery address of logged user.
        /// </summary>
        public async Task<List<Address>> GetAddresses(int? userId)
        {
            List<Address> Addresses = null;
            try
            {
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Name = "Serina Williams",
                        MobileNo = "+1-202-555-0101",
                        DoorNo = "410",
                        Area = "Terry Ave N",
                        City = "New York",
                        State = "California",
                        Country = "USA",
                        PostalCode = "10001",
                        AddressType = "Home"
                    },
                    new Address
                    {
                        Name = "Serina Williams",
                        MobileNo = "+1-356-636-8572",
                        DoorNo = "388",
                        Area = "Anna nagar",
                        City = "Los Angeles",
                        State = "Alabama",
                        Country = "USA",
                        PostalCode = "90004",
                        AddressType = "Office"
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(Addresses);
        }

        /// <summary>
        /// To get added card details.
        /// </summary>
        public async Task<List<UserCard>> GetUserCardsAsync(int userId)
        {
            var PaymentModes = new List<UserCard>
            {
                new UserCard {PaymentMode = "Goldman Sachs Bank Credit Card", CardNumber = "48**********9876"}
            };
            return await Task.FromResult(PaymentModes);
        }

        #endregion
    }
}