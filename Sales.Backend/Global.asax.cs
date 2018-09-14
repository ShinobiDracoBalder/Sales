namespace Sales.Backend
{
    using Sales.Backend.Helpers;
    using Sales.Backend.Migrations;
    using Sales.Backend.Models;
    using Sales.Common.Model;
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocalDataContext, Configuration>());

            this.CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UsersHelper.CheckRole(RolesHelper.ADMIN);
            UsersHelper.CheckRole(RolesHelper.PowerUser);
           
            UsersHelper.CheckSuperUser();
        }
    }
}
