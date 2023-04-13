using System;

using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Register(User entity);
        User login(string email,string password);
        User Update(User getUser, string token);
        User getUserByUID(long userId);
        List<User> GetAllUsers();
        UserDetailViewModel GetUserDetailViewModel(long userId);
        BaseResponseViewModel ChangeUserPassword(long userId,string oldPassword,string newPassword);

        BaseResponseViewModel SaveUserDetails(long userId, UserEditQueryParams userEditQueryParams);
        List<City> GetCities(long countryId);
        public BaseResponseViewModel newContactUsEntry(long userId, string subject, string message);
        public List<MissionIdNameTypeViewModel> getMissionsByUserId(long userId);
        public BaseResponseViewModel addTimeSheetHourData(AddHourVolunteerParams addHour,long userId);
        
        public BaseResponseViewModel editTimeSheetHourData(EditHourVolunteerParams editHour);
        public BaseResponseViewModel deleteTimeSheetHourData(long TimesheetId);

        public List<TimeBasedTimesheetViewModel> GetTimeBasedTimesheets(long userId);
        public List<GoalBasedTimesheetViewModel> GetGoalBasedTimesheets(long userId);

        public BaseResponseViewModel addTimeSheetGoalData(AddGoalVolunteerParams addGoal, long userId);

        public BaseResponseViewModel editTimeSheetGoalData(EditGoalVolunteerParams editGoal);

        public BaseResponseViewModel deleteTimeSheetGoalData(long TimesheetId);


    }
}
