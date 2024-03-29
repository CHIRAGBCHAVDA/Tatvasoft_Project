﻿
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

        IRepository<Mission> Mission { get; }

        IMissionRepository MissionRepo { get; }
        IStoryRepository StoryRepo { get; }

        IAdminRepository AdminRepo { get; }


        void Save(); 
    }
}
