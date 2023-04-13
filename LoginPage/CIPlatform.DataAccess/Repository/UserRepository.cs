using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CIPlatform.DataAccess.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        private readonly CiplatformContext _db;
        public UserDetailViewModel userDetailViewModel;
        private HttpContext _httpContext;

        public UserRepository(CiplatformContext db, HttpContext httpContext) : base(db)
        {
            _db = db;
            _httpContext = httpContext;
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
            var Cities = _db.Cities.AsEnumerable();
            var AllSkills = _db.Skills.ToList();
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
                Availability = user.Availability.Name,
                AllSkills = AllSkills,
                Country = user.CountryId,
                Cities = Cities.ToList(),
                Email = user.Email
            };

            return userDetailViewModel;
        }

        public BaseResponseViewModel ChangeUserPassword(long userId,string oldPassword, string newPassword)
        {
            var user = _db.Users.FirstOrDefault(user => user.UserId==userId);
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            if (BCrypt.Net.BCrypt.Verify(newPassword, user.Password))
            {
                baseResponse.Success = false;
                baseResponse.Message = "New Password can not be same as Older one";
                baseResponse.StatusCode = 500;
            } 
            else if (BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _db.Users.Update(user);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "Password has been changed successfully";
                baseResponse.StatusCode = 200;
            }
            
            else
            {
                baseResponse.Success = false;
                baseResponse.Message = "Old password is incorrect!!";
                baseResponse.StatusCode = 404;
            }

            return baseResponse;
        }

        public BaseResponseViewModel SaveUserDetails(long userId, UserEditQueryParams userEditQueryParams)
        {
            BaseResponseViewModel baseResponse;
            try
            {

                var user = _db.Users.FirstOrDefault(user => user.UserId == userId);
                user.FirstName = userEditQueryParams.name;
                user.LastName = userEditQueryParams.surname;
                user.EmployeeId = userEditQueryParams.eid.ToString();
                user.ManagerDetails = userEditQueryParams.manager;
                user.Title = userEditQueryParams.title;
                user.Department = userEditQueryParams.dept;
                user.ProfileText = userEditQueryParams.myprofile;
                user.WhyIVolunteer = userEditQueryParams.whyIVol;
                user.CityId = userEditQueryParams.cityId;
                user.CountryId = userEditQueryParams.CountryId;
                user.AvailabilityId = (byte?)userEditQueryParams.userAvailabillity;
                user.LinkedInUrl = userEditQueryParams.userLinkedin;
                user.Avatar = userEditQueryParams.Avatar;

                _db.Users.Update(user);

                var userSkills = _db.UserSkills.Where(userSkills => userSkills.UserId == userId).ToList();

                foreach (var userSkill in userSkills)
                {
                    if (Array.IndexOf(userEditQueryParams.skillIds, userSkill.SkillId) < 0)
                    {
                        userSkill.DeletedAt = DateTime.Now;
                        _db.UserSkills.Update(userSkill);
                    }
                }

                foreach (var skillId in userEditQueryParams.skillIds)
                {
                    if (!userSkills.Any(userSkill => userSkill.SkillId == skillId))
                    {
                        var newSkill = new UserSkill()
                        {
                            SkillId = skillId,
                            UserId = userId,
                            Status = 1
                        };
                        _db.UserSkills.Add(newSkill);
                    }
                }

                _db.SaveChanges();
                _httpContext.Session.Remove("userImage");
                _httpContext.Session.SetString("userImage", user.Avatar);

                baseResponse = new BaseResponseViewModel()
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "User Details has been updated"
                };
            }
            catch(Exception ex)
            {
                 baseResponse = new BaseResponseViewModel()
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "User Details could not updated"
                };
            }
            return baseResponse;

        }

        public List<City> GetCities(long countryId)
        {
            var cities = _db.Cities.Where(city => city.CountryId == countryId).ToList();
            return cities;
        }

        public BaseResponseViewModel newContactUsEntry(long userId, string subject, string message)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            try
            {
                ContactU newContactus = new ContactU()
                {
                    UserId = userId,
                    Subject = subject,
                    Message = message
                };

                _db.ContactUs.Add(newContactus);
                _db.SaveChanges();

                baseResponse.Success = true;
                baseResponse.Message = "Thanks for reaching us out...!";
                baseResponse.StatusCode = 200;
            }
            catch(Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Some error filling the form...!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;
        }

        public List<MissionIdNameTypeViewModel> getMissionsByUserId(long userId)
        {
            var user = getUserByUID(userId);
            var toReturn = _db.Users.Where(user => user.UserId == userId)
                            .SelectMany(user => user.MissionApplications.Select(missonApplication => new MissionIdNameTypeViewModel()
                            {
                                MissionId = missonApplication.MissionId,
                                MissionName = missonApplication.Mission.Title,
                                IsGoalBased = missonApplication.Mission.MissionTypeId == 1 ? true : false
                            }));

            return toReturn.ToList();
        }

        public BaseResponseViewModel addTimeSheetHourData(AddHourVolunteerParams addHour,long userId)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            Timesheet timesheet = new Timesheet()
            {
                UserId = userId,
                MissionId = addHour.MissionId,
                Time = addHour.FormattedTime,
                Action = 0,
                DateVolunteered = addHour.VolunteeredDate,
                Notes = addHour.Message,
                ApprovalStatusId = 2,
                CreatedAt = DateTime.Now,
            };

            try
            {
                _db.Timesheets.Add(timesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "New Data has been added in the Timesheet..!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "New Data could not be added in the Timesheet..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;
        }

        public List<TimeBasedTimesheetViewModel> GetTimeBasedTimesheets(long userId)
        {
            var toReturn = _db.Users.Where(user => user.UserId == userId)
                .SelectMany(user => user.Timesheets.Where(tb => tb.Time!=null).Select(TimeBased => new TimeBasedTimesheetViewModel()
                {
                    TimesheetId = TimeBased.TimesheetId,
                    MissionId = TimeBased.MissionId,
                    MissionName = TimeBased.Mission.Title,
                    Time = TimeBased.Time,
                    DateVolunteered = TimeBased.DateVolunteered,
                    Notes = TimeBased.Notes,
                    ApprovalStatusId = TimeBased.ApprovalStatusId,
                    CreatedAt = TimeBased.CreatedAt,
                    UpdatedAt = TimeBased.UpdatedAt,
                    DeletedAt = TimeBased.DeletedAt
                }));
            return toReturn.ToList();
        }
        public List<GoalBasedTimesheetViewModel> GetGoalBasedTimesheets(long userId)
        {
            var toReturn = _db.Users.Where(user => user.UserId == userId)
               .SelectMany(user => user.Timesheets.Where(tb => tb.Time == null).Select(GoalBased => new GoalBasedTimesheetViewModel()
               {
                   TimesheetId = GoalBased.TimesheetId,
                   MissionId = GoalBased.MissionId,
                   MissionName = GoalBased.Mission.Title,
                   Action = GoalBased.Action,
                   DateVolunteered = GoalBased.DateVolunteered,
                   Notes = GoalBased.Notes,
                   ApprovalStatusId = GoalBased.ApprovalStatusId,
                   CreatedAt = GoalBased.CreatedAt,
                   UpdatedAt = GoalBased.UpdatedAt,
                   DeletedAt = GoalBased.DeletedAt
               }));
            return toReturn.ToList();
        }

        public BaseResponseViewModel editTimeSheetHourData(EditHourVolunteerParams editHour)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            var oldTimesheet = _db.Timesheets.First(timesheet => timesheet.TimesheetId == editHour.TimesheetId);
            oldTimesheet.MissionId = editHour.MissionId;
            oldTimesheet.Time = editHour.FormattedTime;
            oldTimesheet.Action = 0;
            oldTimesheet.DateVolunteered = editHour.VolunteeredDate;
            oldTimesheet.Notes = editHour.Message;
            oldTimesheet.ApprovalStatusId = 2;
            oldTimesheet.UpdatedAt = DateTime.Now;


            try
            {
                _db.Timesheets.Update(oldTimesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "Timesheet has been updates successfully...!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet could not be updated..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;

        }

        public BaseResponseViewModel deleteTimeSheetHourData(long TimesheetId)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            try
            {
                var oldTimesheet = _db.Timesheets.First(timesheet => timesheet.TimesheetId == TimesheetId);
                _db.Timesheets.Remove(oldTimesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "Timesheet has been deleted successfully...!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet could not be deleted..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;
        }



        public BaseResponseViewModel addTimeSheetGoalData(AddGoalVolunteerParams addGoal, long userId)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            Timesheet timesheet = new Timesheet()
            {
                UserId = userId,
                MissionId = addGoal.MissionId,
                Action = addGoal.Action,
                DateVolunteered = addGoal.VolunteeredDate,
                Notes = addGoal.Message,
                ApprovalStatusId = 2,
                CreatedAt = DateTime.Now,
            };

            try
            {
                _db.Timesheets.Add(timesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "New Data has been added in the Timesheet..!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "New Data could not be added in the Timesheet..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;

        }

        public BaseResponseViewModel editTimeSheetGoalData(EditGoalVolunteerParams editGoal)
        {

            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            var oldTimesheet = _db.Timesheets.First(timesheet => timesheet.TimesheetId == editGoal.TimesheetId);
            oldTimesheet.MissionId = editGoal.MissionId;
            oldTimesheet.Action = editGoal.Action;
            oldTimesheet.DateVolunteered = editGoal.VolunteeredDate;
            oldTimesheet.Notes = editGoal.Message;
            oldTimesheet.ApprovalStatusId = 2;
            oldTimesheet.UpdatedAt = DateTime.Now;
            try
            {
                _db.Timesheets.Update(oldTimesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "Timesheet has been updates successfully...!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet could not be updated..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;

        }
        public BaseResponseViewModel deleteTimeSheetGoalData(long TimesheetId)
        {

            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            try
            {
                var oldTimesheet = _db.Timesheets.First(timesheet => timesheet.TimesheetId == TimesheetId);
                _db.Timesheets.Remove(oldTimesheet);
                _db.SaveChanges();
                baseResponse.Success = true;
                baseResponse.Message = "Timesheet has been deleted successfully...!!";
                baseResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = "Timesheet could not be deleted..!!";
                baseResponse.StatusCode = 500;
            }
            return baseResponse;
        }
    }
}
