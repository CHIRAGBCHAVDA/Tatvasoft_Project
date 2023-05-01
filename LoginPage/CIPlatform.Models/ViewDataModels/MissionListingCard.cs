using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class MissionListingCard 
    {
        public Mission mission { get; set; }
        public string? MissionTheme { get; set; }
        public string? City { get; set; }
        public MissionSkill missionSkill { get; set; }
        public IEnumerable<string>? Skills { get; set; }
        public string? ImageLink { get; set; }
        public IEnumerable<MissionRating>? rating { get; set; }
        //public bool isFav { get; set; }
        //public long? UserId { get; set; }
        public FavouriteMission favourite { get; set; }
        public DateTime? RegistrationDeadline { get; set; }

    }
}
