using CIPlatform.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly CiplatformContext _db;
        public Repository(CiplatformContext db)
        {
            _db = db;
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Register(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
