using System;

using CIPlatform.Models;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Register(User entity);
        void login(User entity);
        User Update(User getUser, string token);
    }
}
