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

        public ShareStoryViewModel addStoryView()
        {
            ShareStoryViewModel missionNames = new ShareStoryViewModel()
            {
                MissionNames = (from ma in _db.MissionApplications.Where(ma => ma.UserId == long.Parse(_httpContext.Session.GetString("userId")))
                                join m in _db.Missions on ma.MissionId equals m.MissionId
                                select m.Title).Distinct().ToList()
            };

            return missionNames;
        }
        public bool newStorybyUser(string storyMissionName, string storyTitle, DateTime storyDate, string story, string? storyVideoUrl, string[]? srcs)
        {
            try
            {
                var MissionId = _db.Missions.Where(m => m.Title.Equals(storyMissionName)).Select(m => m.MissionId).FirstOrDefault();
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
                    StoryStatusId = 3,
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
                        StoryMedium sMedia = new StoryMedium()
                        {
                            StoryId = _db.Stories.Where(s => s.UserId == UserId && s.MissionId == MissionId && s.Title.Equals(storyTitle)).Select(s => s.StoryId).FirstOrDefault(),
                            Type = "img",
                            Path = src,
                            CreatedAt = DateTime.Now
                        };
                        _db.StoryMedia.Add(sMedia);
                    }
                    _db.SaveChanges();
                }
                
                return true;
            }

            catch (Exception ex)
            {
                return false;
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
    }
}
