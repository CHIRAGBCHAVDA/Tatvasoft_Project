using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;

namespace CIPlatform.DataAccess.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        private readonly CiplatformContext _db;

        public UserRepository(CiplatformContext db) : base(db)
        {
            _db = db;
        }

        public void login(User user)
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            _db.Add(user);
        }

        public void Update(User user,string token)
        {
            user.Token = token;
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
