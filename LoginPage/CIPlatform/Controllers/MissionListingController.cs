using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        public IActionResult PlatformLanding()
        {
            if (HttpContext.Session.GetString("email") != null)
                return View();
            else return RedirectToAction("Index","Home");
        }
    }
}
