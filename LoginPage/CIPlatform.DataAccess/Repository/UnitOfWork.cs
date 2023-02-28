using CIPlatform.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private CiplatformContext _db;
        public UnitOfWork(CiplatformContext db)
        {
            _db = db;
            User = new UserRepository(_db);
        }
        public IUserRepository User { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
