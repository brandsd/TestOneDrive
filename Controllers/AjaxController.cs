using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestOneDrive.Data;

namespace TestOneDrive.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public AjaxController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;

        }
        
    
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ProductList()
        {
            var data = applicationDbContext.ProductRegisters.ToList();
            return new JsonResult(data);
        }
    }
}
