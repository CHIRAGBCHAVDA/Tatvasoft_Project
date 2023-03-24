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
        public List<MissionListingCard> getMissions()
        {
            string myUserId = _httpContext.Session.GetString("userId");

            var missionListingCard = from M in _db.Missions
                                     join C in _db.Cities on M.CityId equals C.CityId
                                     join Tm in _db.MissionThemes on M.MissionThemeId equals Tm.MissionThemeId
                                     join favMissionTbl in (from F in _db.FavouriteMissions
                                                            where F.UserId.ToString() == myUserId
                                                            select F) on M.MissionId equals favMissionTbl.MissionId into favMissionSubTbl
                                     from favMission in favMissionSubTbl.DefaultIfEmpty()
                                     select new MissionListingCard()
                                     {
                                         mission = M,
                                         City = C.Name,
                                         MissionTheme = Tm.Title,
                                         Skills = (List<string>)(from ms in _db.MissionSkills
                                                                 join s in _db.Skills on ms.SkillId equals s.SkillId
                                                                 where ms.MissionId == M.MissionId
                                                                 select s.SkillName),

                                         ImageLink = (from ImgLink in _db.MissionMedia
                                                      where ImgLink.MissionId == M.MissionId
                                                      select ImgLink.MediaPath).FirstOrDefault(),

                                         rating = _db.MissionRatings.Where(m => m.MissionId == M.MissionId).ToList(),

                                         favourite = favMission,
                                     };

            return missionListingCard.ToList();
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
            var newcui = from c in _db.Comments
                         join u in _db.Users on c.UserId equals u.UserId
                         where c.MissionId == missionId
                         select new CommentUserInfo()
                         {
                             comments = c,
                             users = u
                         };
            return newcui.ToList();
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

        public void ApplyMission(long missionId, long userId)
        {
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

            }
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
