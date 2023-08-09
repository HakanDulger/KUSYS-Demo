using System;
using System.Linq.Expressions;

namespace KUSYS_Demo.Data.Abstract
{
	public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
        Task<T> AddAsync(T Entity);
        Task<int> SaveAsync();
    }
}

