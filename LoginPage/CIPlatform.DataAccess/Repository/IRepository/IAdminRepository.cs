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
        public PageList<AdminStoryVM> GetStoryData();
        public IQueryable<AdminUserVM> getAllUserdata();

        public CmsPage getCmsById(long cmsId);
        public bool updateCms(CmsPage cms);
        public bool AddCms(CmsPage cms);

        public IQueryable<AdminCmsVM> getAllCmsdata();
        public List<MissionSkill> getMissionSkills(long missionId);
        public bool AddNewMission(AdminMissionVM missionModel);
        public bool EditMission(AdminMissionVM missionModel);
        public AdminMissionAddButtonDataModel GetAddMissionButtonDataModel();
    }
}
