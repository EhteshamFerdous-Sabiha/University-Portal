using DbExample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace DbExample.Controllers
{
    public class CredentialController : Controller
    {


        
        public IActionResult Login()
        {

            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid == false)

            {
                return View(model);
            }

            if ((model.Username == "admin" && model.Password == "admin") ||
                (model.Username == "manager" && model.Password == "manager"))

            {
                //to track who is logged in
                /*HttpContext.Session.SetString("username", model.Username);
                HttpContext.Session.SetString("role", model.Username);
                return RedirectToAction("Index", "Home");

                */

                //authentocation information


                var claims = new List<Claim>()

                {
                    new Claim(ClaimTypes.Name,model.Username),
                    new Claim(ClaimTypes.Role,model.Username)
                };
                var identity= new ClaimsIdentity(claims,
                    "DbAuth");
                var principal=new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("DbAuth",principal);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(", ","Invalid username or password");

            return View(model);

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            //HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
