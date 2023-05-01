using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
            var stories = from s in _db.Stories where s.StoryStatusId == 3 join m in _db.Missions on s.MissionId equals m.MissionId
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

        //storyMissionName is missionId in story table
        public long draftStorybyUser(string storyMissionName, string storyTitle, DateTime storyDate, string story, string? storyVideoUrl, string[]? srcs)
        {
            var ifAlreadyDrafted = _db.Stories.Where(s => s.MissionId==long.Parse(storyMissionName) && s.UserId==long.Parse(_httpContext.Session.GetString("userId")) && s.StoryStatusId==1).FirstOrDefault();
            if (ifAlreadyDrafted == null)
            {
                try
                {
                    var MissionId = _db.Missions.Where(m => m.MissionId == long.Parse(storyMissionName)).Select(m => m.MissionId).FirstOrDefault();
                    var UserId = long.Parse(_httpContext.Session.GetString("userId"));

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

                    }
                    if (storyVideoUrl != null)
                    {
                        var strArr = storyVideoUrl.Split(',');
                        foreach (var str in strArr)
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

                    }
                    _db.SaveChanges();
                    return newS.MissionId;
                }

                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                try
                {
                    #region update story using efcore
                    //var MissionId = _db.Missions.Where(m => m.MissionId == long.Parse(storyMissionName)).Select(m => m.MissionId).FirstOrDefault();
                    //var UserId = long.Parse(_httpContext.Session.GetString("userId"));

                    //long sid = ifAlreadyDrafted.StoryId;
                    //var toNewDraft = ifAlreadyDrafted;
                    //toNewDraft.MissionId = MissionId;
                    //toNewDraft.UserId = UserId;
                    //toNewDraft.Title = storyTitle;
                    //toNewDraft.Description = story;
                    //toNewDraft.StoryStatusId = 1;
                    //toNewDraft.UpdatedAt = DateTime.Now;

                    //_db.Stories.Update(toNewDraft);
                    //_db.SaveChanges();
                    #endregion

                    var MissionId = _db.Missions
                  .Where(m => m.MissionId == long.Parse(storyMissionName))
                  .Select(m => m.MissionId)
                  .FirstOrDefault();
                    var UserId = long.Parse(_httpContext.Session.GetString("userId"));
                    long sid = ifAlreadyDrafted.StoryId;

                    string query = @"UPDATE story SET mission_id = @missionId, user_id = @userId, title = @title, 
                    description = @description, story_status_id = 1, updated_at = @updatedAt 
                    WHERE story_id = @storyId";

                    _db.Database.ExecuteSqlRaw(query, new SqlParameter("@missionId", MissionId),
                                                         new SqlParameter("@userId", UserId),
                                                         new SqlParameter("@title", storyTitle),
                                                         new SqlParameter("@description", story),
                                                         new SqlParameter("@updatedAt", DateTime.Now),
                                                         new SqlParameter("@storyId", sid));


                    var existingMedia = _db.StoryMedia.Where(s => s.StoryId == ifAlreadyDrafted.StoryId).ToList();

                    #region removeIMG using efcore
                    //foreach (var media in existingMedia)
                    //{
                    //    if (!srcs.Contains(media.Path) && media.Type.Equals("img"))
                    //    {
                    //        media.DeletedAt = DateTime.Now;
                    //        _db.StoryMedia.Update(media);
                    //    }
                    //}
                    #endregion

                    foreach (var media in existingMedia)
                    {
                        if (!srcs.Contains(media.Path) && media.Type.Equals("img"))
                        {
                            var sql = $"UPDATE story_media SET deleted_at = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE story_media_id = {media.StoryMediaId}";
                            _db.Database.ExecuteSqlRaw(sql);
                        }
                    }



                    foreach (var src in srcs)
                    {
                        if (!existingMedia.Any(m => m.Path == src))
                        {
                            StoryMedium sMediaImg = new StoryMedium()
                            {
                                StoryId = ifAlreadyDrafted.StoryId,
                                Type = "img",
                                Path = src,
                                CreatedAt = DateTime.Now
                            };
                            _db.StoryMedia.Add(sMediaImg);
                        }
                    }

                    
                    if (storyVideoUrl != null)
                    {
                        var strArr = storyVideoUrl.Split(',');


                        foreach (var media in existingMedia)
                        {
                            if (!strArr.Contains(media.Path) && media.Type.Equals("vid"))
                            {
                                var sql = $"UPDATE story_media SET deleted_at = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE story_media_id = {media.StoryMediaId}";
                                _db.Database.ExecuteSqlRaw(sql);
                            }
                        }


                        #region remove video url using efcore
                        //foreach (var media in existingMedia)
                        //{
                        //    if(!(strArr.Contains(media.Path)&& media.Type.Equals("vid")))
                        //    {
                        //        media.DeletedAt = DateTime.Now;
                        //        _db.StoryMedia.Update(media) ;
                        //    }
                        //}
                        #endregion

                        foreach (var vurl in strArr)
                        {
                            if (!existingMedia.Any(m => m.Path == vurl))
                            {
                                StoryMedium sMediaVid = new StoryMedium()
                                {
                                    StoryId = ifAlreadyDrafted.StoryId,
                                    Type = "vid",
                                    Path = vurl,
                                    CreatedAt = DateTime.Now
                                };
                                _db.StoryMedia.Add(sMediaVid);
                            }
                        }

                    }

                    _db.SaveChanges();
                    return ifAlreadyDrafted.MissionId;

                }
                catch(Exception ex)
                {
                    return 0;
                }
            }


            
        }

        public void draftStoryByMission(long missionId)
        {
            var storyByMID = _db.Stories.FirstOrDefault(story => story.MissionId == missionId);
            var storyTitle = storyByMID.Title;
            var storyDate = storyByMID.CreatedAt;
            var story = storyByMID.Description;
            var media = _db.StoryMedia.Where(s => s.StoryId == storyByMID.StoryId);
            var videoUrlList = media.Where(m => m.Path.Equals("vid")).Select(m => m.Path).ToList();
            var videoUrl = String.Join(",", videoUrlList);
            var srcs = media.Where(m => m.Path.Equals("img")).Select(m => m.Path).ToArray();

            var temp = draftStorybyUser(missionId.ToString(), storyTitle, storyDate, story, videoUrl, srcs);
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

        public ShareStoryViewModel getDraftedStory(long userId,long?missionId)
        {
            var checkIfDrafted = new Story();
            if (missionId != null)
            {
             checkIfDrafted = _db.Stories.Where(st => st.UserId == userId && st.StoryStatusId==1 && st.MissionId==missionId).FirstOrDefault();
            }
            else
            {
                 checkIfDrafted = _db.Stories.Where(st => st.UserId == userId && st.StoryStatusId == 1).FirstOrDefault();

            }
            if (checkIfDrafted != null)
            {
                var checkIfVideo = _db.StoryMedia.Where(sm => sm.StoryId == checkIfDrafted.StoryId && sm.Type=="vid" && sm.DeletedAt==null).ToList();
                List<string> videoUrls = new List<string>();

                var checkIfImg = _db.StoryMedia.Where(sm => sm.StoryId == checkIfDrafted.StoryId && sm.Type == "img" &&sm.DeletedAt==null).ToList();
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


        public bool storeNewStory(long userId, long missionId)
        {
            try
            {
                var sql = $"UPDATE story SET story_status_id = 2 WHERE user_id = {userId} AND mission_id = {missionId} AND story_status_id = 1";
                _db.Database.ExecuteSqlRaw(sql);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            //var getDrafted = _db.Stories.Where(s => s.UserId == userId && s.MissionId == missionId && s.StoryStatusId == 1).FirstOrDefault();
            //if (getDrafted != null)
            //{
            //    getDrafted.StoryStatusId = 2;
            //    _db.Stories.Update(getDrafted);
            //    _db.SaveChanges();
            //    return true;
            //}
        }

        public Story getStoryBySID(long sid)
        {
            var str = _db.Stories.Where(s => s.StoryId == sid).FirstOrDefault();
            return str;
        }

        public List<StoryMedium> storyMedia(long storyId)
        {
            var toRet = _db.StoryMedia.Where(s => s.StoryId == storyId).ToList();
            return toRet;
        }
    }
}
