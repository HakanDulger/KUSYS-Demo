using System;
using System.Linq.Expressions;
using KUSYS_Demo.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext context;

        public EfGenericRepository(DbContext _context)
		{
            context = _context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}

