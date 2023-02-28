using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class UserRepository : Repository<TblUser>,IUserRepository
    {
        private CiplatformContext _db;

        public UserRepository(CiplatformContext db) : base(db) //here giving me error if :base(db)
        {
            _db = db;
        }

        public void login(TblUser user)
        {
            throw new NotImplementedException();
        }

        public void Register(TblUser user)
        {
            _db.Add(user);
        }

       
    }
}
