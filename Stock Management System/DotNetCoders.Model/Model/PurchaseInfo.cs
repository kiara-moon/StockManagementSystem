using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotNetCoders.Model.Model
{
    public class PurchaseInfo
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please enter purchase date")]
        public DateTime Date { get; set; }
        //[Display(Name = "Code")]
        //[Required(ErrorMessage = "Please enter code")]
        //[MaxLength(4, ErrorMessage = "Code must be in 4 characters")]
        //[MinLength(4, ErrorMessage = "Code must be in 4 characters")]
        public string Code { get; set; }
        [Remote("GetBillNo", "Purchase", AdditionalFields = "Id", ErrorMessage = "This Invoice No is already used")]
        [Display(Name = "Invoice No")]
        [Required(ErrorMessage = "Please enter invoice no")]
        public string InvoiceNo { get; set; }
        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select supplier name")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public List<PurchaseProductInfo> PurchaseProductInfos { get; set; }
    }
}
