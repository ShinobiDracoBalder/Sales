namespace Sales.Backend.Models
{
    using Domain.Model;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Common.Model.Product> Products { get; set; }
    }
}