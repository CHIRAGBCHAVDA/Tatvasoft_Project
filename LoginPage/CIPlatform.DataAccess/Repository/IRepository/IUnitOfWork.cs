
using CIPlatform.Models;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRepository<Country> Country { get; }

        IRepository<City> City { get; }

        IRepository<MissionTheme> MissionTheme { get; }

        IRepository<Skill> Skill { get; }



        void Save(); 
    }
}
