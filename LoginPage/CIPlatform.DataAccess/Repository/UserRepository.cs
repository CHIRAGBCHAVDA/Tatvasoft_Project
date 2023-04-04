using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;

namespace CIPlatform.DataAccess.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        private readonly CiplatformContext _db;
        public UserDetailViewModel userDetailViewModel;

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

        public User getUserByUID(long userId)
        {
            var toReturn = _db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            return toReturn;
        }

        public List<User> GetAllUsers()
        {
            var allU = _db.Users.Where(u => u.UserId > 0).ToList();
            return allU;
        }

        public UserDetailViewModel GetUserDetailViewModel(long userId)
        {
            var user = _db.Users.FirstOrDefault(user => user.UserId == userId);
            var City = _db.Cities.FirstOrDefault(city => city.CityId == user.CityId);
            var Countries = _db.Countries.AsEnumerable();
            var dictOfSkill = new Dictionary<long, string>();
            var userSkills = _db.UserSkills.Where(skill => skill.UserId==user.UserId).Select(skill => skill.Skill).ToList();
            foreach(var skill in userSkills)
            {
                dictOfSkill.Add(skill.SkillId,skill.SkillName);
            }
            var Availabilities = _db.Availabilities.AsEnumerable();

            UserDetailViewModel userDetailViewModel = new UserDetailViewModel()
            {
                UserId = userId,
                Avatar = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmployeeId = user.EmployeeId,
                Manager = user.ManagerDetails,
                Title = user.Title,
                Department = user.Department,
                MyProfile = user.ProfileText,
                WhyIVolunteer = user.WhyIVolunteer,
                CityId = user.CityId,
                City = City.Name,
                Countries = Countries.ToList(),
                Skills = dictOfSkill,
                LinkedIn = user.LinkedInUrl,
                Availabilities = Availabilities.ToList(),
                Availability = user.Availability.Name
            };

            return userDetailViewModel;
        }

        //public UserDetailViewModel getUserDetail(long UserId)
        //{
        //    var user = getUserByUID(UserId);
        //    var City = _db.Cities.FirstOrDefault(city => city.CityId==user.CityId);
        //    var Country = _db.Countries.FirstOrDefault(country => country.CountryId == City.CountryId);
        //    var userDetailViewModel = new UserDetailViewModel()
        //    {
        //        UserId = UserId,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        EmployeeId = user.EmployeeId,
        //        Department = user.Department,
        //        MyProfile = user.ProfileText,
        //        WhyIVolunteer = user.WhyIVolunteer,
        //        CityId = user.CityId,
        //        City = City.Name,


        //    };
        //}  
    }
}
