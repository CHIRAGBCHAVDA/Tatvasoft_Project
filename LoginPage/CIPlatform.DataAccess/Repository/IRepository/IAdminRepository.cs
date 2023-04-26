using CIPlatform.Models;
using CIPlatform.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IAdminRepository
    {
        public PageList<AdminUserVM> GetUserData();
        public PageList<AdminCmsVM> GetCmsData();
        public PageList<AdminMissionVM> GetMissionData();
        public PageList<AdminMissionApplicationVM> GetMissionApplicationData();
  
        public IQueryable<AdminUserVM> getAllUserdata();

        public CmsPage getCmsById(long cmsId);
        public bool updateCms(CmsPage cms);
        public bool AddCms(CmsPage cms);

        public IQueryable<AdminCmsVM> getAllCmsdata();
        public List<MissionSkill> getMissionSkills(long missionId);
        public bool AddNewMission(AdminMissionVM missionModel);
        public bool EditMission(AdminMissionVM missionModel);
        public AdminMissionAddButtonDataModel GetAddMissionButtonDataModel();
        public IQueryable<AdminMissionVM> getAllMissionData();


        public PageList<AdminMissionThemeVM> GetMissionThemeData();
        public IQueryable<AdminMissionThemeVM> getAllMissionThemedata();
        public bool AddNewMissionTheme(AdminMissionThemeVM missionThemeVM);
        public bool EditMissionTheme(AdminMissionThemeVM missionThemeVM);

        public PageList<AdminSkillsViewModel> GetSkillData();
        public IQueryable<AdminSkillsViewModel> getAllSkillData();
        public bool AddNewSkill(AdminSkillsViewModel skillModel);
        public bool EditSkill(AdminSkillsViewModel skillModel);

        public IQueryable<AdminMissionApplicationVM> getAllMissionApplicationData();

        public bool MissionApplicationApprove(long id);
        public bool MissionApplicationReject(long id);


        public PageList<AdminStoryVM> GetStoryData();
        public IQueryable<AdminStoryVM> getAllStoryData();
        public bool StoryApprove(long id);
        public bool StoryReject(long id);

        public IQueryable<AdminBannerViewModel> getBannerData();
        public bool EditBanner(AdminBannerViewModel bannerModel);
        public bool AddBanner(AdminBannerViewModel bannerModel);



    }
}
