using System;
using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    public class MyOrderConfiguration : EntityBaseConfiguration<MyOrderEntity>
    {
        public MyOrderConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.ProductId).IsOptional();
        }
    }
}
