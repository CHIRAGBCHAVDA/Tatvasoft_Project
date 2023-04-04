using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class UserDetailViewModel
    {
        public long UserId { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? EmployeeId { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string MyProfile { get; set; } = string.Empty;
        public string WhyIVolunteer { get; set; } = string.Empty;
        public long? CityId { get; set; }
        public string City { get; set; } = string.Empty;
        public List<Country> Countries { get; set; } = new List<Country>();

        public Dictionary<long, string> Skills { get; set; } = new Dictionary<long, string>();
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public string Availability { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;


    }
}
