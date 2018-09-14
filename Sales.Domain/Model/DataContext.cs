namespace Sales.Domain.Model
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }


        public DbSet<Common.Model.UserType> UserTypes { get; set; }
        public DbSet<Common.Model.User> Users { get; set; }
        public DbSet<Common.Model.Product> Products { get; set; }

    }
}
