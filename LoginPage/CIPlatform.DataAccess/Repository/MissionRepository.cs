using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly CiplatformContext _db;
        public MissionRepository(CiplatformContext db)
        {
            _db = db;
        }
        public List<MissionListingCard> getMissions()
        {
            var missionListingCard = from M in _db.Missions
                                     join C in _db.Cities on M.CityId equals C.CityId
                                     join
                                     Tm in _db.MissionThemes on M.MissionThemeId equals Tm.MissionThemeId

                                     select new MissionListingCard()
                                     {
                                         mission = M,
                                         City = C.Name,
                                         MissionTheme = Tm.Title
                                     };

            return missionListingCard.ToList();
        }
    }
}
