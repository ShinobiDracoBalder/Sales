namespace Sales.ViewModels
{
    using Sales.Common.Model;
    using Sales.Services;

    public class ProductItemViewModel : Product
    {
        #region Attibutes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion
    }
}
