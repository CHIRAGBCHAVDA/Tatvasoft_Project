using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Models.AdminViewModels
{
    public class AdminMainViewModel
    {
        public List<AdminUserVM> AdminUserViewModel { get; set; } = new List<AdminUserVM>();
        public List<AdminCmsVM> AdminCmsViewModel { get; set; } = new List<AdminCmsVM>();
        public List<AdminMissionVM> AdminMissionViewModel { get; set;} = new List<AdminMissionVM>();
        public List<AdminMissionApplicationVM> AdminMissionApplicationViewModel { get; set; } = new List<AdminMissionApplicationVM>();
        public List<AdminStoryVM> AdminStoryViewModel { get; set; } = new List<AdminStoryVM>();
    }
}
