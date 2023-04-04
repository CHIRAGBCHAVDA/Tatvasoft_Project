using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;

using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CiplatformContext _db;
        //public UserDetailViewModel userDetailViewModel;

        public UserController(IUnitOfWork unitOfWork,CiplatformContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        public IActionResult UserEdit()
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                var userDetailViewModel = _unitOfWork.User.GetUserDetailViewModel(long.Parse(HttpContext.Session.GetString("userId")));
                return View(userDetailViewModel);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult SaveEditUser(UserEditQueryParams userEditQueryParams)
        {
            return RedirectToAction("UserEdit","User");
        }
        [HttpPost]
        public IActionResult ChangePassword(string oldPassword,string newPassword)
        {
            return RedirectToAction("UserEdit", "User");
        }
    }
}
