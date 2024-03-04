using Microsoft.AspNetCore.Mvc;

namespace usov_402_pr5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Home/SetCookie")]
        public IActionResult SetCookie(string value, DateTime expiry)
        {
            Response.Cookies.Append("MyCookie", value, new CookieOptions { Expires = expiry });

            return RedirectToAction("Index");
        }

        [HttpGet("/Home/CheckCookie")]
        public IActionResult CheckCookie()
        {
            string value = Request.Cookies["MyCookie"];

            if (value != null)
            {
                ViewData["CookieValue"] = value;
                return View();
            }
            else
            {
                ViewData["CookieValue"] = "Cookie не знайдено.";
                return View();
            }
        }

        [HttpGet("/test-error")]
        public IActionResult TestError()
        {
            throw new Exception("Тестове виключення для перевірки логування.");
        }
    }
}
