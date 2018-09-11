namespace Sales.Domain.Model
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Common.Model.Product> Products { get; set; }
    }
}
