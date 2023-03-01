using CIPlatform.DataAccess.Repository.IRepository;

namespace CIPlatform.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CiplatformContext _db;
        public UnitOfWork(CiplatformContext db)
        {
            _db = db;
            //hi
            User = new UserRepository(_db);
        }
        public IUserRepository User { get; }

        public void Save()
        {
           _db.SaveChanges();
        }
        
    }
}
