namespace Sales.ViewModels
{
    using Sales.Services;


    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private bool isRunning;

        private bool isEnabled;
        #endregion

        #region Properties
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemembered = true;
        }
        #endregion

    }
}
