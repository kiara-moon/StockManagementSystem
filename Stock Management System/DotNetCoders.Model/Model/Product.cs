using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoders.Model.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ReorderLevel { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select category name")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public List<PurchaseProductInfo> PurchaseProductInfos { get; set; }
        public List<SalesProductInfo> SalesProductInfos { get; set; }
    }
}
