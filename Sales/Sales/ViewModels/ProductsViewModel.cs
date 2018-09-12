﻿namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Sales.Common.Model;
    using Sales.Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;
        private ObservableCollection<Product> products;

        //private ObservableCollection<ProductItemViewModel> products;
        #endregion

        #region Properties
        //public List<Product> MyProducts { get; set; }

        //public ObservableCollection<ProductItemViewModel> Products
        //{
        //    get { return this.products; }
        //    set { this.SetValue(ref this.products, value); }
        //}

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion
        
        #region Constructors
        public ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion
        
        #region Singleton
        private static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ProductsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            //var connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    this.IsRefreshing = false;
            //    await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            //    return;
            //}

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Languages.Error", response.Message, "Languages.Accept");
                return;
            }

            this.IsRefreshing = false;

            var list = (List < Product >)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
        #endregion
    }
}