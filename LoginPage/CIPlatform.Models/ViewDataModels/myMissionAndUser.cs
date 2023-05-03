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
        public bool IsApproved;
        public FavouriteMission? FavM;
        public long ratedByUser;
        public string GoalObjective { get; set; } = string.Empty;
        public long Goal { get; set;}
    }
}
