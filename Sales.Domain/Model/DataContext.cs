namespace Sales.Domain.Model
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Sales.Common.Model.Product> Products { get; set; }
    }
}
