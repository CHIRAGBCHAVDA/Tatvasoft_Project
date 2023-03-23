using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class myMissionAndUser
    {
        public MissionListingCard? myMission;
        public List<User>? Users;
        public bool IsApplied;
        public FavouriteMission? FavM;
    }
}
