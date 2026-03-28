using Microsoft.AspNetCore.Mvc;

namespace SpendWise.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
