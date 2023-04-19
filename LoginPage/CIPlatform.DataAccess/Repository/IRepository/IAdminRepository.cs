using CIPlatform.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IAdminRepository
    {
        public PageList<AdminUserVM> GetUserData();
        public PageList<AdminCmsVM> GetCmsData();
        public PageList<AdminMissionVM> GetMissionData();
        public PageList<AdminMissionApplicationVM> GetMissionApplicationData();
        public PageList<AdminStoryVM> GetStoryData();

    }
}
