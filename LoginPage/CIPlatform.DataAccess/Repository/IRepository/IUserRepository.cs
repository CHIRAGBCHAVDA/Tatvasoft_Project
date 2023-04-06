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

    }
}
