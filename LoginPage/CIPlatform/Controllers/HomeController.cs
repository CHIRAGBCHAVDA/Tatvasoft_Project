using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CIPlatform.DataAccess;
using CIPlatform.Models;
using CIPlatform.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
      

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult  LostPassword(User user)
        {
            string resetcode = Guid.NewGuid().ToString();
            var verifyUrl = "/Home/ResetPassword/" + resetcode;
            var link = HttpContext.Request.GetDisplayUrl().Replace(HttpContext.Request.Path, verifyUrl);
            var getUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == user.Email);
            if(getUser != null)
            {
              
                //getUser.Token = resetcode;
                _unitOfWork.User.Update(getUser,resetcode);
                //_unitOfWork.Save();
                var subject = "Password Reset Request";
                var body = "Hi " + getUser.FirstName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " + " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" + "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";
                SendEmail(getUser.Email, body, subject);

                TempData["mail-success"] = "Reset password link has been sent to your email id.";
            }
            else
            {
                TempData["user-not-exists"]= "User doesn't exists.";
                return View();
            }
            return View();
        }

        private void SendEmail(string email, string body, string subject)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("chiragchavda.tatvasoft@gmail.com", "orltrydyhfxgxdrz");
            client.EnableSsl = true;

            var message = new MailMessage();
            message.From = new MailAddress("chiragchavda.tatvasoft@gmail.com");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            client.Send(message);
            

        }

        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult LostPassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(User user)
        {

            string token = HttpContext.Request.GetDisplayUrl().Replace("https://localhost:44383/Home/ResetPassword/", "");
            User getUser = _unitOfWork.User.GetFirstOrDefault(u => u.Token == token);
            if(getUser != null)
            {
                string pwd = BCrypt.Net.BCrypt.HashPassword(user.Password);
                //_unitOfWork.Entry(getUser).Reload();
                var dbContext = (DbContext)_unitOfWork;
                dbContext.Entry(user).State = EntityState.Modified;
                getUser.Password = pwd;
                _unitOfWork.Save();
                TempData["reset-success"] = "Your password has been updated !";
                return RedirectToAction("Index", "Home");
            }


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
        public IActionResult Index(User user)
        {
            User dbUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == user.Email);

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
        public IActionResult RegistrationPOST(User obj)
        {
           
                string pwd = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                obj.Password = pwd;
                _unitOfWork.User.Register(obj);
                _unitOfWork.Save();
                TempData["success"] = "User Added Successfully !";
                return RedirectToAction("Index");
            
            //return View(obj);
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