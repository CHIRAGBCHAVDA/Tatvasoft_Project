using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIPlatform.Models;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<TblUser>
    {
        void Register(TblUser entity);
        void login(TblUser user);
        void save();
    }
}
