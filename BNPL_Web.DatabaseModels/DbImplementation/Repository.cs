using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.DatabaseModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;


namespace Project.DatabaseModel.DbImplementation
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        internal BNPL_Context _context;
        internal DbSet<T> dbSet;
        private DateTime currentTimeStamp;
        private IHttpContextAccessor httpContextAccessor;
        public Repository(BNPL_Context _context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = _context;
            dbSet = _context.Set<T>();
            currentTimeStamp = DateTime.Now;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> where, params string[] paths)
        {
            IQueryable<T> obj = dbSet.Where(where);
            obj = Includes(obj, paths);
            return obj.FirstOrDefault<T>();
        }
        private IQueryable<T> Includes(IQueryable<T> obj, params string[] paths)
        {
            try
            {
                foreach (var path in paths)
                    obj = obj.Include(path);

                obj.Load();
                return obj;
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual IEnumerable<T> GetAll(params string[] paths)
        {
            IQueryable<T> obj = _context.Set<T>();
            obj = Includes(obj, paths);
            return dbSet.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params string[] paths)
        {
            IQueryable<T> obj = dbSet.Where(where);
            obj = Includes(obj, paths);
            return obj;
        }

        public void Add(T obj)
        {
            dbSet.Add(obj);
        }
        public void Dispose()
        {
            _context.Dispose();

        }
        public virtual void Commit()
        {
            SaveChanges();
            

        }
        private int SaveChanges()
        {
            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return _context.SaveChanges();
        }
        
    }
}
