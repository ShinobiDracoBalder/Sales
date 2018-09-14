namespace Sales.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using Sales.Common.Model;

    [NotMapped]
    public class ProductView : Product
    {

        [Display(Name = "ImageFile")]
        public HttpPostedFileBase LifeLogo { get; set; }
    }
}