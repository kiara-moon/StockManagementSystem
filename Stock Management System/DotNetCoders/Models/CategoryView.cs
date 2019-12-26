using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetCoders.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotNetCoders.Models
{
    public class CategoryView
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
        
        public List<Category> Categories { get; set; }
    }
}