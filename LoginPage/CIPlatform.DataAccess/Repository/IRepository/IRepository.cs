using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-user
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        List<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
       
        JsonResult 
    }
}
