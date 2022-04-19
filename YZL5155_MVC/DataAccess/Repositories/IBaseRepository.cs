using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace YZL5155_MVC.DataAccess.Repositories
{
    public interface IBaseRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        T Get(Expression<Func<T, bool>> exp);

        List<T> Gets(Expression<Func<T, bool>> exp);
    }
}
