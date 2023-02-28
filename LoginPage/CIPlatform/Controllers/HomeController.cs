
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CIPlatform.DataAccess;
using CIPlatform.Models;
using CIPlatform.DataAccess.Repository.IRepository;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _db;
      

        public HomeController(IUserRepository db)
        {
            _db = db;
        }


        public IActionResult  LostPassword()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PlatformLanding()
        {
            return View();
        }





        [HttpPost]
        public IActionResult Index(TblUser user)
        {
            TblUser dbUser = _db.GetFirstOrDefault(u => u.Email == user.Email);

            if (dbUser != null && BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                // The password is valid, allow the user to log in
                return RedirectToAction("PlatformLanding", "Home");
            }
            else
            {
                TempData["Error"] = "User Id and Password is not Valid";
                return View();
            }
        }

        [HttpPost, ActionName("Registration")]
        [AutoValidateAntiforgeryToken]
        public IActionResult RegistrationPOST(TblUser obj)
        {
            if (ModelState.IsValid)
            {
                string pwd = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                obj.Password = pwd;
                _db.Register(obj);
                _db.save();
                TempData["success"] = "User Added Successfully !";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public PartialViewResult GetGridView()
        {
            return PartialView("_GridMissionLayout");
        }

        public PartialViewResult GetListView()
        {
            return PartialView("_ListMissionLayout");
        }
        
        public IActionResult VolunteeringMissionPage()
        {
            return View();
        }

        public PartialViewResult CarouselMobileView()
        {
            return PartialView("_CarouselMoblieView");
        }

        public PartialViewResult Header()
        {
            return PartialView("_Header");
        }
        public PartialViewResult Filter()
        {
            return PartialView("_Filter");
        }
        public IActionResult StoryListing()
        {
            return View();
        }
        public PartialViewResult StoryListingCard()
        {
            return PartialView("_StoryListingCard");
        }

        //[HttpPost, ActionName("ResetPassword")]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult ResetPasswordPOST(TblUser obj)
        //{
        //    var usr = _db.TblUsers.SingleOrDefault(u => u.);
        //    if (ModelState.IsValid)
        //    {
        //        _db.TblUsers.Add(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "User Added Successfully !";
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}