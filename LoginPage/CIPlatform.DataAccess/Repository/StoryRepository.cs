using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly CiplatformContext _db;
        private HttpContext _httpContext;

        public StoryRepository(CiplatformContext db, HttpContext httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public List<StoryListingViewModel> getAllStories()
        {
            var stories = from s in _db.Stories join m in _db.Missions on s.MissionId equals m.MissionId
                          join u in _db.Users on s.UserId equals u.UserId
                          join mt in _db.MissionThemes on m.MissionThemeId equals mt.MissionThemeId
                          join c in _db.Cities on m.CityId equals c.CityId
                          
                          select new StoryListingViewModel()
                          {
                             Stories = s,
                             Users = u,
                             Missions = m,
                             Theme = mt.Title,
                             Skills = (List<string>)(from ms in _db.MissionSkills
                                                      join s in _db.Skills on ms.SkillId equals s.SkillId
                                                      where ms.MissionId == m.MissionId
                                                      select s.SkillName),
                             City = c.Name,
                             ImgLink = _db.StoryMedia.Where(x => x.StoryId==s.StoryId).Select(x=>x.Path).First(),
                          };

            return stories.ToList();
        }

        public List<StoryListingViewModel> getStoriesByCountryId(string[] cid)
        {
            var newStories = getAllStories();
            newStories = newStories.FindAll(m => cid.Contains(m.Missions.CountryId.ToString()));

            return newStories;
        }

        public List<StoryListingViewModel> getStoriesPerPage(int pageNum, int pageSize)
        {
            var stories = getAllStories();
            int totalS = stories.Count();
            var storiesPerPage = stories.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            return storiesPerPage;
        }

        public List<Mission> getAppliedMissions(long userId)
        {
            var toReturn = from ma in _db.MissionApplications join m in _db.Missions on ma.MissionId equals m.MissionId where ma.UserId == userId group m by m.MissionId  into g select g.First();
            return toReturn.ToList();
        }

        public long newStorybyUser(string storyMissionName, string storyTitle, DateTime storyDate, string story, string? storyVideoUrl, string[]? srcs)
        {
            try
            {
                var MissionId = _db.Missions.Where(m => m.MissionId== long.Parse(storyMissionName)).Select(m => m.MissionId).FirstOrDefault();
                var UserId = long.Parse(_httpContext.Session.GetString("userId"));

                MissionApplication newMA = new MissionApplication()
                {
                    MissionId = MissionId,
                    UserId = UserId,
                    AppliedAt = storyDate,
                    ApprovalStatusId = 1,
                    CreatedAt = DateTime.Now
                };

                Story newS = new Story()
                {
                    UserId = UserId,
                    MissionId = MissionId,
                    Title = storyTitle,
                    Description = story,
                    StoryStatusId = 1,
                    PublishedAt = DateTime.Now,
                    CreatedAt = storyDate
                };

                _db.MissionApplications.Add(newMA);
                _db.Stories.Add(newS);
                _db.SaveChanges();

                if (srcs != null)
                {
                    foreach (var src in srcs)
                    {
                        StoryMedium sMediaImg = new StoryMedium()
                        {
                            StoryId = _db.Stories.Where(s => s.UserId == UserId && s.MissionId == MissionId && s.Title.Equals(storyTitle)).Select(s => s.StoryId).FirstOrDefault(),
                            Type = "img",
                            Path = src,
                            CreatedAt = DateTime.Now
                        };
                        _db.StoryMedia.Add(sMediaImg);
                    }
                    _db.SaveChanges();
                }
                if (storyVideoUrl != "")
                {
                    var strArr = storyVideoUrl.Split(',');
                    foreach(var str in strArr)
                    {
                        StoryMedium sMediaVideo = new StoryMedium()
                        {
                            StoryId = _db.Stories.Where(s => s.UserId == UserId && s.MissionId == MissionId && s.Title.Equals(storyTitle)).Select(s => s.StoryId).FirstOrDefault(),
                            Type = "vid",
                            Path = str,
                            CreatedAt = DateTime.Now
                        };
                        _db.StoryMedia.Add(sMediaVideo);
                    }
                    _db.SaveChanges();
                }

                return newS.MissionId;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }


        public bool deleteStory(long storyId)
        {
            try
            {
                //var std = _db.Stories.Where(s => s.StoryId == storyId).FirstOrDefault();
                //_db.Stories.Remove(std);
                //_db.SaveChanges();

                //var std = _db.Stories.Find(storyId);
                //_db.Stories.Remove(std);
                //_db.SaveChanges();

                var query = "DELETE from story where story_id = {0}";
                var res = _db.Database.ExecuteSqlRaw(query,storyId);
                return res > 0;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public ShareStoryViewModel getDraftedStory(long userId)
        {
            var checkIfDrafted = _db.Stories.Where(st => st.UserId == userId && st.StoryStatusId==1).FirstOrDefault();
            if (checkIfDrafted != null)
            {
                var checkIfVideo = _db.StoryMedia.Where(sm => sm.StoryId == checkIfDrafted.StoryId && sm.Type=="vid").ToList();
                List<string> videoUrls = new List<string>();

                var checkIfImg = _db.StoryMedia.Where(sm => sm.StoryId == checkIfDrafted.StoryId && sm.Type == "img").ToList();
                List<string> imgSrcs = new List<string>();

                if (checkIfVideo != null)
                {
                    foreach (var v in checkIfVideo)
                    {
                        videoUrls.Add(v.Path);
                    }
                }
                if (checkIfImg != null)
                {
                    foreach (var i in checkIfImg)
                    {
                        imgSrcs.Add(i.Path);
                    }
                }

                if (checkIfDrafted != null)
                {
                    var toReturn = new ShareStoryViewModel()
                    {
                        StoryTitle = checkIfDrafted.Title,
                        Date = checkIfDrafted.CreatedAt,
                        MyStory = checkIfDrafted.Description,
                        VideoUrl = videoUrls,
                        Photos = imgSrcs,
                        MissionId = checkIfDrafted.MissionId
                    };
                    return toReturn;
                }

            }

           

            return null;
        }

    }
}
