using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestOneDrive.Models.Product
{
    public class Image
    {
        [Key]
        public int ProductId { get; set; }  //Primary key
        
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
