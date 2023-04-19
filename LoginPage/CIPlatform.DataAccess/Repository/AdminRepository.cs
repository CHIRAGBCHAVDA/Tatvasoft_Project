using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models.AdminViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CiplatformContext _db;
        private HttpContext _httpContext;
        public AdminRepository(CiplatformContext db, HttpContext httpContext)
        {
            _db = db;
            _httpContext = httpContext;

        }

        public PageList<AdminCmsVM> GetCmsData()
        {
            var cmsRecords = _db.CmsPages.Select(cms => new AdminCmsVM()
            {
                CMSId = cms.CmsPageId,
                Title = cms.Title,
                Description = cms.Desciption,
                Slug = cms.Slug,
                Status = cms.Status
            });
            var cmsData = new PageList<AdminCmsVM>()
            {
                Records = cmsRecords.ToList(),
                Count = cmsRecords.Count()
            };

            return cmsData;
        }

        public PageList<AdminMissionApplicationVM> GetMissionApplicationData()
        {
            var missionApplicationRecords = _db.MissionApplications.Select(missionApplication => new AdminMissionApplicationVM()
            {
                MissionApplicationId = missionApplication.MissionApplicationId,
                MissionId = missionApplication.MissionId,
                MissionTitle = missionApplication.Mission.Title,
                AppliedDate = missionApplication.AppliedAt,
                UserId = missionApplication.UserId,
                UserName = missionApplication.User.FirstName+missionApplication.User.LastName
            });

            var missionApplicationData = new PageList<AdminMissionApplicationVM>()
            {
                Records = missionApplicationRecords.ToList(),
                Count = missionApplicationRecords.Count()
            };

            return missionApplicationData;
        }

        public PageList<AdminMissionVM> GetMissionData()
        {
            var missionRecords = _db.Missions.Select(mission => new AdminMissionVM()
            {
                MissionId = mission.MissionId,
                MissionTitle = mission.Title,
                MissionTypeId = mission.MissionTypeId,
                StartDate = mission.StartDate,
                EndDate =mission.EndDate
            });

            var missionData = new PageList<AdminMissionVM>()
            {
                Records = missionRecords.ToList(),
                Count = missionRecords.Count()
            };

            return missionData;
        }

        public PageList<AdminStoryVM> GetStoryData()
        {
            var storyRecords = _db.Stories.Select(story => new AdminStoryVM()
            {
                StoryId = story.StoryId,
                MissionId = story.MissionId,
                StoryTitle = story.Title,
                FullName = story.User.FirstName+story.User.LastName,
                MissionTitle = story.Mission.Title
            });

            var storyData = new PageList<AdminStoryVM>()
            {
                Records = storyRecords.ToList(),
                Count = storyRecords.Count()
            };

            return storyData;
        }

        public PageList<AdminUserVM> GetUserData()
        {
            var CountryList = _db.Countries.ToList();
            var CityList = _db.Cities.ToList();
            var userRecords = _db.Users.Select(user => new AdminUserVM()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Department = user.Department,
                Email = user.Email,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
                Avatar = user.Avatar,
                CityId = user.CityId,
                CountryId = user.CountryId,
                ProfileText = user.ProfileText,
                Countries = CountryList,
                Cities = CityList,
            });

            var userData = new PageList<AdminUserVM>()
            {
                Records = userRecords.ToList(),
                Count = userRecords.Count()
            };

            return userData;
        }

    }
}
