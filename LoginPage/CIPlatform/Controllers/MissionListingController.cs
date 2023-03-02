using CIPlatform.DataAccess.Repository;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CIPlatform.Controllers
{
    public class MissionListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public MissionListingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult PlatformLanding()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                MissionListingHeader Model = new MissionListingHeader();
                List<Country> country = _unitOfWork.Country.GetAll();
                List<City> city = _unitOfWork.City.GetAll();
                List<MissionTheme> theme = _unitOfWork.MissionTheme.GetAll();
                List<Skill> skill = _unitOfWork.Skill.GetAll();
                Model.Country = country;
                Model.City = city;
                Model.MissionTheme = theme;
                Model.Skills = skill;
                return View(Model);

            }
            else return RedirectToAction("Index","Home");

        }
    }
}
