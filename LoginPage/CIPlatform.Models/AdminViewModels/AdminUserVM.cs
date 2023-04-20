using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminUserVM
    {
        public long UserId { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string? LastName { get; set; } = string.Empty;
        [Required(ErrorMessage ="This field is required")]
        public string? Email { get; set; } = string.Empty;
        public string? EmployeeId { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public bool? Status{ get; set; } = false;
        public string? Avatar { get;set; } = string.Empty;
        public long CityId { get; set; }
        public long CountryId { get; set; }
        public string? ProfileText { get; set; } = string.Empty;

        //[Required(ErrorMessage ="Enter A valid Password")]
        //public string Password { get; set; }

        //[Required(ErrorMessage ="This Field is required")]
        //[Compare("Password",ErrorMessage ="Password and Confirm Password must be matched")]
        //public string ConfirmPassword { get; set; }

        public List<Country> Countries { get; set; } = new List<Country>();
        public List<City> Cities { get; set; } = new List<City>();

    }
}
