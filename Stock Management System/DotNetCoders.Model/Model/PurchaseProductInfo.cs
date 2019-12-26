using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model.Model
{
    public class PurchaseProductInfo
    {
        public int Id { get; set; }
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please enter quantity")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Quantity must be a number")]
        public int Quantity { get; set; }
        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please enter unit price")]
        [RegularExpression("^([0-9]*)+(\\.([0-9]*)+)?$", ErrorMessage = "Unit price must be a number")]
        public double UnitPrice { get; set; }
        [Display(Name = "MRP")]
        [Required(ErrorMessage = "Please enter mrp")]
        [RegularExpression("^([0-9]*)+(\\.([0-9]*)+)?$", ErrorMessage = "Unit price must be a number")]
        public double MRP { get; set; }
        [Display(Name = "Manufactured Date")]
        [Required(ErrorMessage = "Please enter manufactured date")]
        public DateTime ManufacturedDate { get; set; }
        [Display(Name = "Expire Date")]
        [Required(ErrorMessage = "Please enter expire date")]
        public DateTime ExpiredDate { get; set; }
        //public string Code { get; set; }
        public string Remarks { get; set; }
        public int PurchaseInfoId { get; set; }
        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please select product name")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public PurchaseInfo PurchaseInfo { get; set; }
    }
}
