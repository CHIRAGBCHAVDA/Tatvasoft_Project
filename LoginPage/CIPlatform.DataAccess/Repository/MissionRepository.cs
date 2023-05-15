using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CIPlatform.DataAccess.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly CiplatformContext _db;
        private HttpContext _httpContext;
        public MissionRepository(CiplatformContext db, HttpContext httpContext)
        {
            _db = db;
            _httpContext = httpContext;

        }
        public IQueryable<MissionListingCard> getMissions()
        {
            string myUserId = _httpContext.Session.GetString("userId");

            var missionListingCard = _db.Missions.Where(m => m.DeletedAt == null).Select(mission => new MissionListingCard()
            {
                mission = mission,
                GetGoalMission = mission.GoalMissions.First(),
                City = mission.City.Name,
                MissionTheme = mission.MissionTheme.Title,
                Skills = mission.MissionSkills.Select(missionSkills => missionSkills.Skill.SkillName),
                ImageLink = mission.MissionMedia.FirstOrDefault(m => m.MediaType.Equals("img")).MediaPath,
                //ImageLink = mission.MissionMedia.First(m => m.MediaType.Equals("img")).Select(m => m.MediaPath).ToString(),
                rating = mission.MissionRatings,
                favourite = mission.FavouriteMissions.First(),
                RegistrationDeadline = mission.RegistrationDeadline,
                IsFavourite = mission.FavouriteMissions.Where(m => m.UserId == long.Parse(myUserId) && m.DeletedAt == null).FirstOrDefault() != null ? true : false,
                isApplied = mission.MissionApplications.FirstOrDefault(ma => ma.UserId == long.Parse(myUserId) && ma.ApprovalStatusId==2) != null ? true:false,
                isPending = mission.MissionApplications.FirstOrDefault(ma => ma.UserId == long.Parse(myUserId) && ma.ApprovalStatusId==1) != null ? true:false,
              }); 

            //var missionListingCard = from M in _db.Missions
            //                         join C in _db.Cities on M.CityId equals C.CityId
            //                         join Tm in _db.MissionThemes on M.MissionThemeId equals Tm.MissionThemeId
            //                         join favMissionTbl in (from F in _db.FavouriteMissions
            //                                                where F.UserId.ToString() == myUserId
            //                                                select F) on M.MissionId equals favMissionTbl.MissionId into favMissionSubTbl
            //                         from favMission in favMissionSubTbl.DefaultIfEmpty()
            //                         select new MissionListingCard()
            //                         {
            //                             mission = M,
            //                             City = C.Name,
            //                             MissionTheme = Tm.Title,
            //                             Skills = (List<string>)(from ms in _db.MissionSkills
            //                                                     join s in _db.Skills on ms.SkillId equals s.SkillId
            //                                                     where ms.MissionId == M.MissionId
            //                                                     select s.SkillName),

            //                             ImageLink = (from ImgLink in _db.MissionMedia
            //                                          where ImgLink.MissionId == M.MissionId
            //                                          select ImgLink.MediaPath).FirstOrDefault(),

            //                             rating = _db.MissionRatings.Where(m => m.MissionId == M.MissionId).ToList(),

            //                             favourite = favMission,
            //                         };

            return missionListingCard;
        }

        public List<string> getSkills(long id)
        {
            var a = from ms in _db.MissionSkills
                    join s in _db.Skills on ms.SkillId equals s.SkillId
                    where ms.MissionId == id
                    select s.SkillName;
            return a.ToList();
        }

        public List<CommentUserInfo> CommentByMissionUserId(long missionId)
        {
            var isApproved = _db.MissionApplications.FirstOrDefault(m => m.MissionId == missionId && m.ApprovalStatusId == 2 && m.UserId == long.Parse(_httpContext.Session.GetString("userId"))) != null ? true : false;
            var newcui = from c in _db.Comments
                         join u in _db.Users on c.UserId equals u.UserId
                         where c.MissionId == missionId
                         select new CommentUserInfo()
                         {
                             comments = c,
                             users = u,
                             IsApproved = isApproved
                         };
            return newcui.OrderByDescending(c => c.comments.CreatedAt).ToList();
        }

        public void AddComment(string comment, long missionId, long userId)
        {
            Comment toAdd = new Comment()
            {
                UserId = userId,
                MissionId = missionId,
                CommentDescription = comment,
                CreatedAt = DateTime.Now,
            };
            _db.Comments.Add(toAdd);
        }

        public bool ApplyMission(long missionId, long userId)
        {
            var user = _db.Users.First(user => user.UserId == userId);
            if (user.Status ==false)
            {
                return false;
            }

            var mission = _db.Missions.First(m => m.MissionId == missionId);
            

            var checkIfExist = _db.MissionApplications.Where(mapp => mapp.UserId == userId && mapp.MissionId == missionId).FirstOrDefault();
            if (checkIfExist == null)
            {
                MissionApplication missionApplication = new MissionApplication()
                {
                    MissionId = missionId,
                    AppliedAt = DateTime.Now,
                    UserId = userId,
                    ApprovalStatusId = 1,
                    CreatedAt = DateTime.Now
                };

                _db.MissionApplications.Add(missionApplication);
                
                return true;
            }

            return false;
        }

        public void UpdateRate(MissionRating missionRating)
        {
            missionRating.UpdatedAt = DateTime.Now;
            _db.MissionRatings.Update(missionRating);

        }
        public void addRate(MissionRating missionRating)
        {
            _db.MissionRatings.Add(missionRating);
        }
    }
}
