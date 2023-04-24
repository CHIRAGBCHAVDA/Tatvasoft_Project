using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMissionAddButtonDataModel
    {
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public List<MissionTheme> MissionThemes { get; set; } = new List<MissionTheme>();
    }
}
