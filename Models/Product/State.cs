using System.ComponentModel.DataAnnotations;

namespace TestOneDrive.Models.Product
{
    public class State
    {
        [Key]
        public int Id { get; set; } //Primary key
        public string Name { get; set; }    
        
        public Country Country { get; set; } //Foregine key 
       
    }
}
