using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminUserVM
    {
        public long UserId { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public bool? Status{ get; set; }

    }
}
