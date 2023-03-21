using System.ComponentModel.DataAnnotations;

namespace TestOneDrive.Models.Account
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
