using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestOneDrive.Models;
using TestOneDrive.Models.Account;
using TestOneDrive.Models.Product;
using TestOneDrive.Models.ViewModel;
using TestOneDrive.ViewModels;



namespace TestOneDrive.Data
{
    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options ):base(options)
        {
            
                
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ProductRegister> ProductRegisters { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        
        
        
        
        
        
    }
}
