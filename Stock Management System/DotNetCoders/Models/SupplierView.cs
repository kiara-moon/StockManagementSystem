using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetCoders.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotNetCoders.Models
{
    public class SupplierView
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
        [Display(Name = " Address ")]
        public string Address { get; set; }
        [Display(Name = " Email ")]
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress]
        [RegularExpression("^[_A-Za-z'`+-.]+([_A-Za-z0-9'+-.]+)*@([A-Za-z0-9-])+(\\.[A-Za-z0-9]+)*(\\.([A-Za-z]*){3,})$", ErrorMessage = "Enter proper email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format not proper of email address")]
        public string Email { get; set; }
        [Display(Name = " Contact ")]
        [Required(ErrorMessage = "Please enter contact no")]
        [RegularExpression("([01][0-9]*)", ErrorMessage = "Point must be digits")]
        [MaxLength(11, ErrorMessage = "Contact number must be in 11 digits")]
        [MinLength(11, ErrorMessage = "Contact number must be in 11 digits")]
        public string Contact { get; set; }
        [Display(Name = " Contact Person")]
        public string ContactPerson { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}