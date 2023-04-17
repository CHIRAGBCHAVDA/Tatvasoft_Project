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
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var baseResponse = _unitOfWork.User.SaveUserDetails(userId,userEditQueryParams);
            return Json(baseResponse);
        }
        [HttpPost]
        public IActionResult ChangePassword(string oldPassword,string newPassword)
        {
            var userId = long.Parse((HttpContext.Session.GetString("userId")));
            var baseResponse = _unitOfWork.User.ChangeUserPassword(userId, oldPassword, newPassword);
            return Json(baseResponse);
        }

        [HttpPost]
        public IActionResult GetCities(long countryId)
        {
            var cities = _unitOfWork.User.GetCities(countryId);
            return Json(cities);
        }

        public PartialViewResult GetHeader()
        {
            return PartialView("_Header");
        }

        [HttpPost]
        public IActionResult ContactUs(long userId, string subject, string message)
        {
            var baseResponse = _unitOfWork.User.newContactUsEntry(userId, subject, message);
            return Json(baseResponse);
        }


        public IActionResult VolunteeringTimesheet()
        {
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var customTimesheet = new CustomTimesheetViewModel()
            {
                GoalBasedTimesheets = _unitOfWork.User.GetGoalBasedTimesheets(userId),
                TimeBasedTimesheets = _unitOfWork.User.GetTimeBasedTimesheets(userId)
            };
            return View(customTimesheet);
        }

        [HttpPost]
        public IActionResult getMissionApplicationsByUserId()
        {
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var toReturn = _unitOfWork.User.getMissionsByUserId(userId);
            return Json(toReturn);
        }

        [HttpPost]
        public IActionResult AddHourVolunteer(AddHourVolunteerParams addHourParams)
        {
            var baseResponse = _unitOfWork.User.addTimeSheetHourData(addHourParams, long.Parse(HttpContext.Session.GetString("userId")));
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-add-success"] = baseResponse.Message;
                return PartialView("_TimeBasedTimesheet", _unitOfWork.User.GetTimeBasedTimesheets(userId));
            }
            return null;
        }

        [HttpPost]
        public IActionResult EditHourVolunteer(EditHourVolunteerParams Editparams)
        {
            var baseResponse = _unitOfWork.User.editTimeSheetHourData(Editparams);
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-update-success"] = baseResponse.Message;
                return PartialView("_TimeBasedTimesheet", _unitOfWork.User.GetTimeBasedTimesheets(userId));
            }
            return null;
        }
        [HttpPost]
        public IActionResult DeleteHourVolunteer(long TimesheetId)
        {
            var baseResponse = _unitOfWork.User.deleteTimeSheetHourData(TimesheetId);
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-delete-success"] = baseResponse.Message;
                return PartialView("_TimeBasedTimesheet", _unitOfWork.User.GetTimeBasedTimesheets(userId));
            }
            return null;
        }

        [HttpPost]
        public IActionResult AddGoalVolunteer(AddGoalVolunteerParams addGoalParams)
        {
            var baseResponse = _unitOfWork.User.addTimeSheetGoalData(addGoalParams, long.Parse(HttpContext.Session.GetString("userId")));
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-add-success"] = baseResponse.Message;
                return PartialView("_GoalBasedTimesheet", _unitOfWork.User.GetGoalBasedTimesheets(userId));
            }
            return null;
        }

        [HttpPost]
        public IActionResult EditGoalVolunteer(EditGoalVolunteerParams Editparams)
        {
            var baseResponse = _unitOfWork.User.editTimeSheetGoalData(Editparams);
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-update-success"] = baseResponse.Message;
                return PartialView("_GoalBasedTimesheet", _unitOfWork.User.GetGoalBasedTimesheets(userId));
            }
            return null;
        }

        [HttpPost]
        public IActionResult DeleteGoalVolunteer(long TimesheetId)
        {
            var baseResponse = _unitOfWork.User.deleteTimeSheetGoalData(TimesheetId);
            if (baseResponse.Success)
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));
                TempData["hour-delete-success"] = baseResponse.Message;
                return PartialView("_GoalBasedTimesheet", _unitOfWork.User.GetGoalBasedTimesheets(userId));
            }
            return null;
        }

        [HttpPost]
        public DateTime getAppliedDateByMissionId(long MissionId)
        {
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var UserAppliedDate = _unitOfWork.User.getAppliedDateByMissionId(userId, MissionId);
            return UserAppliedDate;
        }
    }
}
