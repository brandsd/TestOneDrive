using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TestOneDrive.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [Remote(action: "RemoteValidation", controller: "Account")]   // for RemoteValidation validation
        public string Username { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid Email Address")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter Valid Mobile Number")]
        [Required(ErrorMessage = "Please Enter MobileNumber")]
        public long? MobileNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = ("Confirm Password can't matched !"))]
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
