using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System;
using TestOneDrive.Models.Account;
using TestOneDrive.Models.ViewModel;
using TestOneDrive.Data;
using System.Linq;

namespace TestOneDrive.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDNTCaptchaValidatorService validatorService;
        public AccountController(ApplicationDbContext applicationDbContext, IDNTCaptchaValidatorService validatorService)
        {
            this.applicationDbContext = applicationDbContext;
            this.validatorService = validatorService;
        }
        public IActionResult Users()
        {
            var a = applicationDbContext.Accounts.ToList();
            return View(a);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateDNTCaptcha(ErrorMessage ="Please enter security code!", CaptchaGeneratorLanguage =
        //    Language.English,CaptchaGeneratorDisplayMode =DisplayMode.ShowDigits)]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    TempData["captchaError"] = "Please enter valid security key";
                    return View(model);
                }
                var log = applicationDbContext.Accounts.Where(a => a.Username == model.Username).SingleOrDefault();
                if (log != null)
                {
                    bool isValid = (log.Username == model.Username && DecryptPassword(log.Password) == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var pri = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pri);
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorPassword"] = "Please enter correct  Password !";
                        return View(model);
                    }
                }
                else
                {
                    TempData["ErrorUsername"] = "Username not found !";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

        }
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {

                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }
        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var store = Request.Cookies.Keys;
            foreach (var cookies in store)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Login", "Account");
        }

        [AcceptVerbs("Post", "Get")]
        public IActionResult RemoteValidation(string username)   // for RemoteValidation validation
        {
            var data = applicationDbContext.Accounts.Where(a => a.Username == username).SingleOrDefault();
            if (data != null)
            {
                return Json($"Username{username} Already in use !");
            }
            else
            {
                return Json(true);
            }

        }
        public IActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new Account()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = EncryptPassword(model.Password),
                    MobileNumber = (long)model.MobileNumber,
                    IsActive = model.IsActive
                };
                applicationDbContext.Accounts.Add(result);
                applicationDbContext.SaveChanges();
                TempData["successMessage"] = "You are eligible to login, Please fill own credential's then Login !";
                return RedirectToAction("Login");

            }
            else
            {
                TempData["ErrorMessage"] = "Please Entered all information !";
                return View(model);
            }

        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
