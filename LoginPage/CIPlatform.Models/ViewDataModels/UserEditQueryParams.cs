using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.ViewDataModels
{
    public class UserEditQueryParams
    {
        public string name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public long eid { get; set; }
        public string manager { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string dept { get; set; } = string.Empty;
        public string myprofile { get; set; } = string.Empty;
        public string whyIVol { get; set; } = string.Empty;
        public long cityId { get; set; }
        public long CountryId { get; set; }
        public long userAvailabillity { get; set; }
        public string userLinkedin { get; set; } = string.Empty;
        public long[] skillIds { get; set; } = new long[0];

    }
}
