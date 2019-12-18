using ShoppingCart.Models;
using SQLite;
using Xamarin.Forms;

namespace ShoppingCart.DataService
{
    /// <summary>
    /// This class is used to maintain the data in local.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SQLiteDatabase
    {
        private static SQLiteDatabase database;

        public static SQLiteDatabase Shared
        {
            get
            {
                if (database == null) database = new SQLiteDatabase();
                return database;
            }
        }

        private SQLiteConnection Connection { get; set; }

        public bool Initialized { get; private set; }

        /// <summary>
        /// Create and initialize the local database connection.
        /// Create table.
        /// </summary>
        public void Init()
        {
            Connection = DependencyService.Get<ILocalStorage>().GetConnection();
            Connection.CreateTable<UserInfo>();
            Initialized = true;
        }

        #region

        /// <summary>
        /// To get user logged details from local database.
        /// </summary>
        public UserInfo GetUserInfo()
        {
            UserInfo userInfo = null;
            if (Connection != null) userInfo = Connection.Table<UserInfo>().FirstOrDefault();
            return userInfo;
        }

        /// <summary>
        /// Insert user details.
        /// </summary>
        public void InsertUserDetail(UserInfo userInfo)
        {
            if (Connection != null) Connection.Insert(userInfo);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        public void UpdateUserDetail(UserInfo userInfo)
        {
            if (Connection != null) Connection.Update(userInfo);
        }

        #endregion
    }
}