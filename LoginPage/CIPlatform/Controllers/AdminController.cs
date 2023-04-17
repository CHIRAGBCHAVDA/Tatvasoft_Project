using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
