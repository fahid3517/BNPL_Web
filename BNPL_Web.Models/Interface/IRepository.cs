using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Add(T obj);

        void Update(T obj);

        void Delete(T entity);

        void Commit();

        void Delete(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where, params string[] paths);
        IEnumerable<T> GetAll(params string[] paths);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, params string[] paths);
    }

}
