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

        public User login(string email,string password)
        {
            User dbUser = _db.Users.FirstOrDefault(u => u.Email == email);
            if (dbUser != null && BCrypt.Net.BCrypt.Verify(password, dbUser.Password))
            {
                return dbUser;
            }
            
            return null;

        }

        public void Register(User user)
        {
            string pwd = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = pwd;
            _db.Add(user);
        }

        public User Update(User user,string token)
        {
            User temp = _db.Users.FirstOrDefault(x => x.UserId == user.UserId);
            temp.TokenCreatedAt = DateTime.Now;
            temp.Token = token;
            
            _db.Users.Update(temp);
            _db.SaveChanges();
            
            return temp;
        }
    }
}
