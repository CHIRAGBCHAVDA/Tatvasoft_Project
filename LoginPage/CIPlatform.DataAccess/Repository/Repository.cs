﻿using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<T> dbSet;


        public Repository(CiplatformContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query;
        }

        public List<City> GetAllCity()
        {
            var cities = _db.Cities.ToList();
            return cities;
        }

        public List<Country> GetAllCountry()
        {
            var countries = _db.Countries.ToList();
            return countries;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        
    }
}
