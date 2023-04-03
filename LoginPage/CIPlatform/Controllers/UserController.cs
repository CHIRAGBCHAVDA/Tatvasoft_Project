using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserEdit()
        {
            return View();
        }
    }
}
