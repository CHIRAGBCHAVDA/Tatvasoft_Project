using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CiplatformContext _db;
        
        public UnitOfWork(CiplatformContext db,IHttpContextAccessor httpContext)
        {
            _db = db;
            User = new UserRepository(_db,httpContext.HttpContext);
            Country = new Repository<Country>(_db);
            City = new Repository<City>(_db);
            Skill = new Repository<Skill>(_db);
            MissionTheme = new Repository<MissionTheme>(_db);
            MissionRepo = new MissionRepository(_db,httpContext.HttpContext);
            StoryRepo = new StoryRepository(_db,httpContext.HttpContext);
            AdminRepo = new AdminRepository(_db, httpContext.HttpContext);
        }
        public IUserRepository User { get; }

        public IRepository<Country> Country { get; }
        public IRepository<City> City { get; }
        public IRepository<Skill> Skill { get; }
        public IRepository<MissionTheme> MissionTheme { get; }

        public IRepository<Mission> Mission { get; }

        public IMissionRepository MissionRepo { get; }

        public IStoryRepository StoryRepo { get; }
        public IAdminRepository AdminRepo { get; }

        public void Save()
        {
           _db.SaveChanges();
        }
        
    }
}
