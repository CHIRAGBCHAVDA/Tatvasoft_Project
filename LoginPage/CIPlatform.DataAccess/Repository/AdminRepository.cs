using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
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
            var cmsRecords = _db.CmsPages.Where(cms => cms.DeletedAt == null).Select(cms => new AdminCmsVM()
            {
                CMSId = cms.CmsPageId,
                Title = cms.Title,
                Description = cms.Desciption,
                Slug = cms.Slug,
                Status = (bool)cms.Status
            });
            var cmsData = new PageList<AdminCmsVM>()
            {
                Records = cmsRecords.Take(10).ToList(),
                Count = cmsRecords.Count()
            };

            return cmsData;
        }

        public PageList<AdminMissionApplicationVM> GetMissionApplicationData()
        {
            var missionApplicationRecords = _db.MissionApplications.Where(application => application.ApprovalStatusId == 1).Select(missionApplication => new AdminMissionApplicationVM()
            {
                MissionApplicationId = missionApplication.MissionApplicationId,
                MissionId = missionApplication.MissionId,
                MissionTitle = missionApplication.Mission.Title,
                AppliedDate = missionApplication.AppliedAt,
                UserId = missionApplication.UserId,
                UserName = missionApplication.User.FirstName + missionApplication.User.LastName
            });

            var missionApplicationData = new PageList<AdminMissionApplicationVM>()
            {
                Records = missionApplicationRecords.Skip(0).Take(10).ToList(),
                Count = missionApplicationRecords.Count()
            };
            return missionApplicationData;
        }

        public IQueryable<AdminMissionApplicationVM> getAllMissionApplicationData()
        {
            var missionApplicationRecords = _db.MissionApplications.Where(application => application.ApprovalStatusId==1).Select(missionApplication => new AdminMissionApplicationVM()
            {
                MissionApplicationId = missionApplication.MissionApplicationId,
                MissionId = missionApplication.MissionId,
                MissionTitle = missionApplication.Mission.Title,
                AppliedDate = missionApplication.AppliedAt,
                UserId = missionApplication.UserId,
                UserName = missionApplication.User.FirstName + missionApplication.User.LastName
            });
            return missionApplicationRecords;
        }

        public PageList<AdminMissionVM> GetMissionData()
        {
            var Countries = _db.Countries;
            var Cities = _db.Cities;
            var Availabilities = _db.Availabilities;
            var Skills = _db.Skills;
            var missionThemes = _db.MissionThemes;

            var missionRecords = _db.Missions.Where(m => m.DeletedAt==null).Select(mission => new AdminMissionVM()
            {
                MissionId = mission.MissionId,
                MissionTitle = mission.Title,
                MissionThemeId = mission.MissionThemeId,
                MissionThemes = missionThemes.ToList(),
                MissionTypeId = mission.MissionTypeId,
                CountryId = mission.CountryId,
                CityId = mission.CityId,
                AvailableSeats = mission.AvailableSeats,
                RegistrationDeadline = mission.RegistrationDeadline,
                Countries = Countries.ToList(),
                Cities = Cities.ToList(),
                Availabilities = Availabilities.ToList(),
                Skills = Skills.ToList(),
                Goal = mission.GoalMissions.FirstOrDefault().GoalValue,
                GoalObjective = mission.GoalMissions.FirstOrDefault().GoalObjectiveText,
                AvailabilityId = (byte)mission.AvailabilityId,
                Status = mission.Status,
                OrganizationName = mission.OrganizationName,
                OrganizationDetail = mission.OrganizationDetail,
                ShortDescription = mission.ShortDescription,
                LongDescription = mission.Description,
                VideoUrl = mission.MissionMedia.AsQueryable().Where(media => media.MediaType.Equals("vid")).Select(url => url.MediaPath).ToList(),
                Photos = mission.MissionMedia.AsQueryable().Where(media => media.MediaType.Equals("img")).Select(src => src.MediaPath).ToList(),
                StartDate = (DateTime)mission.StartDate,
                EndDate = (DateTime)mission.EndDate,
            });

            var missionData = new PageList<AdminMissionVM>()
            {
                Records = missionRecords.Skip(0).Take(10).ToList(),
                Count = missionRecords.Count()
            };

            return missionData;
        }

        public PageList<AdminStoryVM> GetStoryData()
        {
            var storyRecords = _db.Stories.Where(story => story.StoryStatusId == 2).Select(story => new AdminStoryVM()
            {
                StoryId = story.StoryId,
                MissionId = story.MissionId,
                StoryTitle = story.Title,
                FullName = story.User.FirstName + story.User.LastName,
                MissionTitle = story.Mission.Title,
                UserId = story.User.UserId,
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
            var userRecords = _db.Users.Where(user => user.DeletedAt == null).Select(user => new AdminUserVM()
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
                Records = userRecords.Take(10).ToList(),
                Count = userRecords.Count()
            };

            return userData;
        }
        public IQueryable<AdminUserVM> getAllUserdata()
        {
            var CountryList = _db.Countries.ToList();
            var CityList = _db.Cities.ToList();

            var userRecords = _db.Users.Where(user => user.DeletedAt==null).Select(user => new AdminUserVM()
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
            return userRecords;
        }

        public IQueryable<AdminCmsVM> getAllCmsdata()
        {
            var cmsRecords = _db.CmsPages.Where(cms => cms.DeletedAt==null).Select(cms => new AdminCmsVM()
            {
                CMSId = cms.CmsPageId,
                Title = cms.Title,
                Description = cms.Desciption,
                Slug = cms.Slug,
                Status = (bool)cms.Status
            });

            return cmsRecords;
        }

        public IQueryable<AdminMissionThemeVM> getAllMissionThemedata()
        {
            var missionThemeRecords = _db.MissionThemes.Where(mt => mt.DeletedAt==null).Select(theme => new AdminMissionThemeVM()
            {
                MissionThemeId = theme.MissionThemeId,
                Status = theme.Status,
                Title = theme.Title
            });

            return missionThemeRecords;
        }


        public CmsPage getCmsById(long cmsId)
        {
            var cms = _db.CmsPages.FirstOrDefault(cmsPage => cmsPage.CmsPageId == cmsId);
            return cms;
        }
        public bool updateCms(CmsPage cms)
        {
            try
            {
                _db.CmsPages.Update(cms);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddCms(CmsPage cms)
        {
            try
            {
                _db.CmsPages.Add(cms);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MissionSkill> getMissionSkills(long missionId)
        {
            var missionSkills = _db.MissionSkills.Where(skill => skill.MissionId == missionId);
            return missionSkills.ToList();
        }

        public bool AddNewMission(AdminMissionVM missionModel)
        {
            try
            {

                Mission newMission = new Mission()
                {
                    MissionThemeId = missionModel.MissionThemeId,
                    CityId = missionModel.CityId,
                    CountryId = missionModel.CountryId,
                    Title = missionModel.MissionTitle,
                    ShortDescription = missionModel.ShortDescription,
                    Description = missionModel.LongDescription,
                    StartDate = missionModel.StartDate,
                    EndDate = missionModel.EndDate,
                    MissionTypeId = missionModel.MissionTypeId,
                    Status = missionModel.Status,
                    OrganizationName = missionModel.OrganizationName,
                    OrganizationDetail = missionModel.OrganizationDetail,
                    AvailabilityId = missionModel.AvailabilityId,
                    CreatedAt = DateTime.Now,
                    AvailableSeats = missionModel.AvailableSeats,
                    RegistrationDeadline = missionModel.RegistrationDeadline,
                };
                _db.Missions.Add(newMission);
                _db.SaveChanges();

                if (newMission.MissionTypeId == 2)
                {
                    var goalMission = new GoalMission()
                    {
                        MissionId = newMission.MissionId,
                        GoalObjectiveText = missionModel.GoalObjective,
                        GoalValue = (int)missionModel.Goal,
                        CreatedAt = DateTime.Now,
                    };
                    _db.GoalMissions.Add(goalMission);
                }
                

                foreach (var skillId in missionModel.skillids)
                {
                    MissionSkill newMissionSkill = new MissionSkill()
                    {
                        MissionId = newMission.MissionId,
                        SkillId = skillId,
                        CreatedAt = DateTime.Now
                    };
                    _db.MissionSkills.Add(newMissionSkill);
                }
                _db.SaveChanges();
                var counter = 0;
                foreach (var img in missionModel.missionphotos)
                {
                    MissionMedium newMedia = new MissionMedium()
                    {
                        MissionId = newMission.MissionId,
                        MediaName = "MissionId" + missionModel.MissionId + "Media" + counter,
                        MediaType = "img",
                        MediaPath = img,
                        Default = true,
                        CreatedAt = DateTime.Now,
                    };
                    counter++;
                    _db.MissionMedia.Add(newMedia);
                }
                foreach (var vid in missionModel.videourls)
                {
                    MissionMedium newMedia = new MissionMedium()
                    {
                        MissionId = newMission.MissionId,
                        MediaName = "MissionId" + missionModel.MissionId + "Media" + counter,
                        MediaType = "vid",
                        MediaPath = vid,
                        Default = true,
                        CreatedAt = DateTime.Now,
                    };
                    counter++;
                    _db.MissionMedia.Add(newMedia);
                }


                if (missionModel.files != null && missionModel.files.Count() > 0)
                {
                    var fileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MissionFiles");
                    if (!Directory.Exists(fileFolder))
                    {
                        Directory.CreateDirectory(fileFolder);
                    }
                    else
                    {
                        for (int i = 0; i < missionModel.files.Count(); i++)
                        {
                            var fileName = newMission.MissionId + " - " + missionModel.files[i].FileName;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MissionFiles", fileName);

                            if (!File.Exists(filePath))
                            {
                                missionModel.files[i].CopyTo(new FileStream(fileName, FileMode.Create));
                                MissionDocument newMissionDoc = new MissionDocument()
                                {
                                    MissionId = newMission.MissionId,
                                    DocumentName = fileName,
                                    DoucmentType = "pdf",
                                    DocumentPath = filePath,
                                    CreatedAt = DateTime.Now
                                };

                                _db.MissionDocuments.Add(newMissionDoc);
                            }
                        }
                    }
                }

                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditMission(AdminMissionVM missionModel)
        {
            try
            {
                var oldMission = _db.Missions.FirstOrDefault(mission => mission.MissionId == missionModel.MissionId);
                oldMission.Title = missionModel.MissionTitle;
                oldMission.MissionThemeId = missionModel.MissionThemeId;
                oldMission.CityId = missionModel.CityId;
                oldMission.CountryId = missionModel.CountryId;
                oldMission.ShortDescription = missionModel.ShortDescription;
                oldMission.Description = missionModel.LongDescription;
                oldMission.StartDate = missionModel.StartDate;
                oldMission.EndDate = missionModel.EndDate;
                oldMission.MissionTypeId = missionModel.MissionTypeId;
                oldMission.Status = missionModel.Status;
                oldMission.OrganizationName = missionModel.OrganizationName;
                oldMission.OrganizationDetail = missionModel.OrganizationDetail;
                oldMission.AvailabilityId = missionModel.AvailabilityId;
                oldMission.UpdatedAt = DateTime.Now;
                oldMission.AvailableSeats = missionModel.AvailableSeats;
                oldMission.RegistrationDeadline = missionModel.RegistrationDeadline;

                _db.Missions.Update(oldMission);
                _db.SaveChanges();

                if (oldMission.MissionTypeId == 2)
                {
                    var goalMission = _db.GoalMissions.FirstOrDefault(goalM => goalM.MissionId == oldMission.MissionId);
                    if (goalMission != null)
                    {
                        goalMission.GoalValue = (int)missionModel.Goal;
                        goalMission.GoalObjectiveText = missionModel.GoalObjective;
                        goalMission.UpdatedAt = DateTime.Now;
                        _db.GoalMissions.Update(goalMission);
                    }
                    else
                    {
                        var newGoalMission = new GoalMission()
                        {
                            MissionId = oldMission.MissionId,
                            GoalValue = (int)missionModel.Goal,
                            GoalObjectiveText = missionModel.GoalObjective,
                            CreatedAt = DateTime.Now,
                        };
                        _db.GoalMissions.Add(newGoalMission);
                    }
                    
                }


                var oldMissionImg = _db.MissionMedia.Where(media => media.MissionId == oldMission.MissionId).ToList();
                _db.MissionMedia.RemoveRange(oldMissionImg);


                var counter = 0;
                foreach (var img in missionModel.missionphotos)
                {
                    MissionMedium newMedia = new MissionMedium()
                    {
                        MissionId = missionModel.MissionId,
                        MediaName = "MissionId" + missionModel.MissionId + "Media" + counter,
                        MediaType = "img",
                        MediaPath = img,
                        Default = true,
                        CreatedAt = DateTime.Now,
                    };
                    counter++;
                    _db.MissionMedia.Add(newMedia);
                }
                foreach (var vid in missionModel.videourls)
                {
                    MissionMedium newMedia = new MissionMedium()
                    {
                        MissionId = missionModel.MissionId,
                        MediaName = "MissionId" + missionModel.MissionId + "Media" + counter,
                        MediaType = "vid",
                        MediaPath = vid,
                        Default = true,
                        CreatedAt = DateTime.Now,
                    };
                    counter++;
                    _db.MissionMedia.Add(newMedia);
                }

                var oldMissionFiles = _db.MissionDocuments.Where(document => document.MissionId == oldMission.MissionId).ToList();
                _db.MissionDocuments.RemoveRange(oldMissionFiles);

                if (missionModel.files != null && missionModel.files.Count() > 0)
                {
                    var fileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MissionFiles");
                    if (!Directory.Exists(fileFolder))
                    {
                        Directory.CreateDirectory(fileFolder);
                    }
                    else
                    {
                        for (int i = 0; i < missionModel.files.Count(); i++)
                        {
                            var fileName = missionModel.MissionId + " - " + missionModel.files[i].FileName;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MissionFiles", fileName);

                            if (!File.Exists(filePath))
                            {
                                missionModel.files[i].CopyTo(new FileStream(fileName, FileMode.Create));
                                MissionDocument newMissionDoc = new MissionDocument()
                                {
                                    MissionId = missionModel.MissionId,
                                    DocumentName = fileName,
                                    DoucmentType = "pdf",
                                    DocumentPath = filePath,
                                    CreatedAt = DateTime.Now
                                };

                                _db.MissionDocuments.Add(newMissionDoc);
                            }
                        }
                    }
                }

                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public AdminMissionAddButtonDataModel GetAddMissionButtonDataModel()
        {
            

            var toReturn = new AdminMissionAddButtonDataModel()
            {
                MissionThemes = _db.MissionThemes.ToList(),
                Availabilities = _db.Availabilities.ToList(),
                Cities = _db.Cities.ToList(),
                Countries = _db.Countries.ToList(),
                Skills = _db.Skills.ToList()
            };

            return toReturn;
        }

        public PageList<AdminMissionThemeVM> GetMissionThemeData()
        {
            var missionThemes = _db.MissionThemes.Where(tm => tm.DeletedAt==null).Select(theme => new AdminMissionThemeVM()
            {
                MissionThemeId = theme.MissionThemeId,
                Status = theme.Status,
                Title = theme.Title
            });
            var toReturn = new PageList<AdminMissionThemeVM>()
            {
                Records = missionThemes.Take(10).ToList(),
                Count = missionThemes.Count()
            };

            return toReturn;
        }

        public bool AddNewMissionTheme(AdminMissionThemeVM missionThemeVM)
        {
            try
            {
                var newMissionTheme = new MissionTheme()
                {
                    Title = missionThemeVM.Title,
                    Status = missionThemeVM.Status,
                    CreatedAt = DateTime.Now,
                };
                _db.MissionThemes.Add(newMissionTheme);
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool EditMissionTheme(AdminMissionThemeVM missionThemeVM)
        {
            try
            {
                var missionTheme = _db.MissionThemes.FirstOrDefault(theme => theme.MissionThemeId == missionThemeVM.MissionThemeId);
                missionTheme.Title = missionThemeVM.Title;
                missionTheme.Status = missionThemeVM.Status;
                missionTheme.UpdatedAt = DateTime.Now;

                _db.MissionThemes.Update(missionTheme);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PageList<AdminSkillsViewModel> GetSkillData()
        {
            var skills = _db.Skills.Where(s => s.DeletedAt==null).Select(skill => new AdminSkillsViewModel()
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                Status = skill.Status
            });

            var toReturn = new PageList<AdminSkillsViewModel>()
            {
                Records = skills.Skip(0).Take(10).ToList(),
                Count = skills.Count()
            };
            return toReturn;
        }

        public IQueryable<AdminSkillsViewModel> getAllSkillData()
        {
            var skills = _db.Skills.Where(s => s.DeletedAt == null).Select(skill => new AdminSkillsViewModel()
            {
                SkillId = skill.SkillId,
                SkillName = skill.SkillName,
                Status = skill.Status
            });

            return skills;
        }

        public bool AddNewSkill(AdminSkillsViewModel skillModel)
        {
            try
            {
                var newSkill = new Skill()
                {
                    SkillName = skillModel.SkillName,
                    Status = skillModel.Status,
                    CreatedAt = DateTime.Now
                };
                _db.Skills.Add(newSkill);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditSkill(AdminSkillsViewModel skillModel)
        {
            try
            {
                var skill = _db.Skills.FirstOrDefault(skill => skill.SkillId == skillModel.SkillId);
                skill.SkillName = skillModel.SkillName;
                skill.Status = skillModel.Status;
                skill.UpdatedAt = DateTime.Now;

                _db.Skills.Update(skill);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool MissionApplicationApprove(long id)
        {
            try
            {
                var MissionApplication = _db.MissionApplications.FirstOrDefault(application => application.MissionApplicationId == id);
                MissionApplication.ApprovalStatusId = 2;
                _db.MissionApplications.Update(MissionApplication);

                var mission = _db.Missions.FirstOrDefault(m => m.MissionId == MissionApplication.MissionId);
                if (mission.MissionTypeId == 1)
                {
                    mission.AvailableSeats = mission.AvailableSeats - 1;
                    _db.Missions.Update(mission);
                }
                _db.SaveChanges();

                var checkToNotify = _db.UserNotificationPrefs.FirstOrDefault(noti => noti.UserId == MissionApplication.UserId && noti.NotificationId == 8);
                if(checkToNotify != null)
                {
                    UserNotification notification = new UserNotification()
                    {
                        UserId = MissionApplication.UserId,
                        NotificationId = 8,
                        NotificationText = "Mission Application for " + MissionApplication.Mission.Title + " is Approved!!",
                        NotificationAnchorId = MissionApplication.MissionId,
                        NotificationDate = DateTime.Now,
                        MarkedAsRead = 0
                    };

                    _db.UserNotifications.Add(notification);
                    _db.SaveChanges();
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public bool MissionApplicationReject(long id)
        {
            try
            {
                var MissionApplication = _db.MissionApplications.FirstOrDefault(application => application.MissionApplicationId == id);
                MissionApplication.ApprovalStatusId = 3;
                _db.MissionApplications.Update(MissionApplication);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IQueryable<AdminStoryVM> getAllStoryData()
        {
            var toReturn = _db.Stories.Where(story => story.StoryStatusId==2).Select(story => new AdminStoryVM()
            {
                StoryId = story.StoryId,
                MissionId = story.MissionId,
                StoryTitle = story.Title,
                FullName = story.User.FirstName + story.User.LastName,
                MissionTitle = story.Mission.Title,
                UserId = story.User.UserId
            });

            return toReturn;
        }

        public bool StoryApprove(long id)
        {
            try
            {
                var story = _db.Stories.FirstOrDefault(story => story.StoryId == id);
                story.StoryStatusId = 3;
                _db.Stories.Update(story);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool StoryReject(long id)
        {
            try
            {
                var story = _db.Stories.FirstOrDefault(story => story.StoryId == id);
                story.StoryStatusId = 4;
                _db.Stories.Update(story);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IQueryable<AdminBannerViewModel> getBannerData()
        {
            var toReturn = _db.Banners.Where(banner => banner.DeletedAt==null).Select(banner => new AdminBannerViewModel()
            {
                BannerId = banner.BannerId,
                Heading = banner.Heading,
                Image = banner.Image,
                ShortDescription = banner.ShortDescription
            });

            return toReturn;
        }

        public bool EditBanner(AdminBannerViewModel bannerModel)
        {
            try
            {
                var banner = _db.Banners.FirstOrDefault(banner => banner.BannerId == bannerModel.BannerId);
                banner.Image = bannerModel.Image;
                banner.Heading = bannerModel.Heading;
                banner.ShortDescription = bannerModel.ShortDescription;
                banner.UpdatedAt = DateTime.Now;

                _db.Banners.Update(banner);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddBanner(AdminBannerViewModel bannerModel)
        {
            try
            {
                var banner = new Banner()
                {
                    Image = bannerModel.Image,
                    Heading = bannerModel.Heading,
                    ShortDescription = bannerModel.ShortDescription,
                    CreatedAt = DateTime.Now,
                };
                _db.Banners.Add(banner);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<AdminMissionVM> getAllMissionData()
        {
            var Countries = _db.Countries;
            var Cities = _db.Cities;
            var Availabilities = _db.Availabilities;
            var Skills = _db.Skills;
            var missionThemes = _db.MissionThemes;

            var missionRecords = _db.Missions.Where(m => m.DeletedAt==null).Select(mission => new AdminMissionVM()
            {
                MissionId = mission.MissionId,
                MissionTitle = mission.Title,
                MissionThemeId = mission.MissionThemeId,
                MissionThemes = missionThemes.ToList(),
                MissionTypeId = mission.MissionTypeId,
                CountryId = mission.CountryId,
                CityId = mission.CityId,
                AvailableSeats = mission.AvailableSeats,
                RegistrationDeadline = mission.RegistrationDeadline,
                Countries = Countries.ToList(),
                Cities = Cities.ToList(),
                Availabilities = Availabilities.ToList(),
                Skills = Skills.ToList(),
                Goal = mission.GoalMissions.FirstOrDefault().GoalValue,
                GoalObjective = mission.GoalMissions.FirstOrDefault().GoalObjectiveText,
                AvailabilityId = (byte)mission.AvailabilityId,
                Status = mission.Status,
                OrganizationName = mission.OrganizationName,
                OrganizationDetail = mission.OrganizationDetail,
                ShortDescription = mission.ShortDescription,
                LongDescription = mission.Description,
                VideoUrl = mission.MissionMedia.AsQueryable().Where(media => media.MediaType.Equals("vid")).Select(url => url.MediaPath).ToList(),
                Photos = mission.MissionMedia.AsQueryable().Where(media => media.MediaType.Equals("img")).Select(src => src.MediaPath).ToList(),
                StartDate = (DateTime)mission.StartDate,
                EndDate = (DateTime)mission.EndDate,
            });
            return missionRecords;
        }

        public bool GetUserEditFormPost(AdminUserVM userEditParams)
        {
            var user = _db.Users.First(u=> u.UserId==userEditParams.UserId);
            user.FirstName = userEditParams.FirstName;
            user.LastName = userEditParams.LastName;
            user.Department = userEditParams.Department;
            user.Email = userEditParams.Email;
            user.EmployeeId = userEditParams.EmployeeId;
            user.CityId = userEditParams.CityId;
            user.CountryId = userEditParams.CountryId;
            user.Status = userEditParams.Status;
            user.Avatar = userEditParams.Avatar;
            user.ProfileText = userEditParams.ProfileText;

            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GetUserAddFormPost(AdminUserAddViewModel userAddParams)
        {
            string pwd = BCrypt.Net.BCrypt.HashPassword(userAddParams.Password);
            var user = new User()
            {
                FirstName = userAddParams.FirstName,
                LastName = userAddParams.LastName,
                Department = userAddParams.Department,
                Email = userAddParams.Email,
                EmployeeId = userAddParams.EmployeeId,
                CityId = (long)userAddParams.CityId,
                CountryId = (long)userAddParams.CountryId,
                Status = userAddParams.Status,
                Avatar = userAddParams.Avatar,
                ProfileText = userAddParams.ProfileText,
                Password = pwd
            };
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool DeleteUser(long userId)
        {
            try
            {
                var user = _db.Users.First(u => u.UserId == userId);
                user.DeletedAt = DateTime.Now;
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool DeleteCms(long cmsId)
        {
            try
            {
                var cms = _db.CmsPages.FirstOrDefault(cms => cms.CmsPageId == cmsId);
                cms.DeletedAt = DateTime.Now;
                _db.CmsPages.Update(cms);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteMission(long missionId)
        {
            try
            {
                var mission = _db.Missions.FirstOrDefault(mission => mission.MissionId == missionId);
                mission.DeletedAt = DateTime.Now;
                _db.Missions.Update(mission);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteMissionTheme(long missionthemeId)
        {
            try
            {
                var missionTheme = _db.MissionThemes.FirstOrDefault(mission => mission.MissionThemeId == missionthemeId);
                missionTheme.DeletedAt = DateTime.Now;
                _db.MissionThemes.Update(missionTheme);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteMissionSkill(long missionskillId)
        {
            try
            {
                var missionSkill = _db.Skills.FirstOrDefault(skill => skill.SkillId == missionskillId);
                missionSkill.DeletedAt = DateTime.Now;
                _db.Skills.Update(missionSkill);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteBanner(long bannerId)
        {
            try
            {
                var banner = _db.Banners.FirstOrDefault(banner => banner.BannerId == bannerId);
                banner.DeletedAt = DateTime.Now;
                _db.Banners.Update(banner);
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }

}
