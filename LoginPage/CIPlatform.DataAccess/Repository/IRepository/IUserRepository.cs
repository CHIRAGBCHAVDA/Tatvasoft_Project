using System;

using CIPlatform.Models;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Register(User entity);
        User login(string email,string password);
        User Update(User getUser, string token);
        User getUserByUID(long userId);
        List<User> GetAllUsers();
    }
}
