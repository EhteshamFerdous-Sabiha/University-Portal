using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DbExample.Controllers
{
    public class ScController : Controller
    {
        public IActionResult SetData()
        {
            Response.Cookies.Append("Email","ehteshamferdous11@gmail.com");

            //Session
            HttpContext.Session.SetString("UserName", "Ehtesham");

            return Content("Set Cookie");
        }


        public IActionResult GetData()
        {

            var option = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };

            string email=Request.Cookies["Email"]??"CE";
            string userName = HttpContext.Session.GetString("UserName")??"UN";
            return Content("cookie"+email+ "username" +userName);
        }
    }
}
