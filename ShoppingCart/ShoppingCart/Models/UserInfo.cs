using SQLite;

namespace ShoppingCart.Models
{
    public class UserInfo
    {
        [PrimaryKey] [AutoIncrement] public int ID { get; set; }

        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public bool IsNewUser { get; set; }
    }
}