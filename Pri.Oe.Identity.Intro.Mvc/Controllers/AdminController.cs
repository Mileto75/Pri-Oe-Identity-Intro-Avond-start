using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pri.Oe.Identity.Intro.Mvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tools()
        {
            return View();
        }
    }
}
