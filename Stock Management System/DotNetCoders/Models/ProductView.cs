using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DotNetCoders.Model.Model;
using System.ComponentModel.DataAnnotations;


namespace DotNetCoders.Models
{
    public class ProductView
    {
        public int Id { get; set; }

        [Display(Name = " Code ")]
        [Required(ErrorMessage = "Please enter code")]
        [MaxLength(4, ErrorMessage = "Code must be in 4 characters")]
        [MinLength(4, ErrorMessage = "Code must be in 4 characters")]
        public string Code { get; set; }

        [Display(Name = " Name ")]
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Please enter reorder level")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Reorder Level must be a positive number")]
        public int ReorderLevel { get; set; }


        public string Description { get; set; }


        public int CategoryId { get; set; }
        public List<Product> Products { get; set; }

        public List<SelectListItem> CategorySelectListItems { get; set; }

        public ProductView()        {
            
            Products = new List<Product>();
            CategorySelectListItems = new List<SelectListItem>();
        }















    }
}