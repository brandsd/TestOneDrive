using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestOneDrive.Models.Product
{
    public class ProductRegister
    {
        [Key]
        [Required(ErrorMessage = "Please enter Product Id")]
        public int ProductId { get; set; }  //Primary key
        [Required(ErrorMessage ="Please enter Product code")]
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }
        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter Email Address")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Please enter vailid Email Address")]
        [DisplayName("Email Address")]
        public string CustomerEmail { get; set; }
        [DisplayName("Mobile Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter valid mobile number")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Please enter Product name")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        //[Required(ErrorMessage = "Please enter Product Description")]
        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Please enter Product Category")]
        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }
        [Required(ErrorMessage = "Please enter Product Price")]
        [DisplayName("Product Price")]
        public long Price { get; set; }
        [Required(ErrorMessage = "Please select Product Manufucturing Date")]
        [DisplayName("Manufucturing Date")]
        public DateTime ManufucturingDate { get; set; }
        [Required(ErrorMessage = "Please select Product Expiry Date")]
        [DisplayName("Expire Date")]
        public DateTime ExpireDate { get; set; }
        [Required(ErrorMessage = "Please select Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please select State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please select City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter First Addess")]
        [DisplayName("First Address")]
        public string Addess1 { get; set; }
        [Required(ErrorMessage = "Please enter Second Address")]
        [DisplayName("Second Address")]
        public string Addess2 { get; set; }
        [Required(ErrorMessage = "Please enter Landmark")]
        public string Landmark { get; set; }
        //for fileupload
        [DisplayName("Document Name")]
        //[Required(ErrorMessage = "Please enter Image Name")]
        public string ImageName { get; set; }
        public string FilePath { get; set; }

        
        
        [NotMapped]
        public IFormFile ImagePath { get; set; }











    }
}
