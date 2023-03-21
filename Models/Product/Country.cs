using System.ComponentModel.DataAnnotations;

namespace TestOneDrive.Models.Product
{
    public class Country
    {
        [Key]
        public int Id { get; set; } //Primary key
        public string Name { get; set; }
        
    }
}
