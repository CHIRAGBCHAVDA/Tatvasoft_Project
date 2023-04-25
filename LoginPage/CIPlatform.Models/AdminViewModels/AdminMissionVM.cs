using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMissionVM
    {
        public long MissionId { get; set; }
        [Required(ErrorMessage ="Mission Title is required.")]
        public string MissionTitle { get; set; }
        public Byte MissionTypeId { get; set; }
        public long MissionThemeId { get; set; }
        public List<MissionTheme> MissionThemes { get; set; } = new List<MissionTheme>();


        public long CityId { get; set; }
        public long CountryId { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<string>? VideoUrl { get; set; }
        public List<string>? Photos { get; set; }

        public bool Status { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDetail { get; set; }
        public byte AvailabilityId { get; set; }
        public int? AvailableSeats { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public long? Goal { get; set; }
        public string? GoalObjective { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<Country> Countries { get; set; } = new List<Country>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
        //public Dictionary<long, string>? MissionSkills { get; set; } = new Dictionary<long, string>();

        public List<IFormFile> files{ get; set; } = new List<IFormFile>();
        public List<string> missionphotos { get; set; } = new List<string>();
        public List<long> skillids { get; set; } = new List<long>();
        public List<string> videourls { get; set; } = new List<string>();


    }
}
