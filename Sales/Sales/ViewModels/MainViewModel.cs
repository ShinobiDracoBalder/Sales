﻿namespace Sales.ViewModels
{
    public class MainViewModel
    {
        #region View Models
        public ProductsViewModel Products { get; set; }

        //public AddProductViewModel AddProduct { get; set; }

        //public EditProductViewModel EditProduct { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Products = new ProductsViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
