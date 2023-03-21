using System.ComponentModel.DataAnnotations;

namespace TestOneDrive.Models.Product
{
    public class City
    {
        [Key]
        public int Id { get; set; }//Primary key
        public string Name { get; set; }    
        public State State { get; set; } //Foregine key 
        
         
    }
}
