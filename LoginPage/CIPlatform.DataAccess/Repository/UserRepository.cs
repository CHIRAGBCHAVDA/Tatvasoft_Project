using CIPlatform.Data;
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

        public User Update(User user,string token)
        {
            //user.Token = token;
            User temp = _db.Users.FirstOrDefault(x => x.UserId == user.UserId);
            temp.Token = token;
            //temp.CreatedAt = DateTime.Now;
            
             _db.Users.Update(temp);
             _db.SaveChanges();
            return temp;
        }
    }
}
