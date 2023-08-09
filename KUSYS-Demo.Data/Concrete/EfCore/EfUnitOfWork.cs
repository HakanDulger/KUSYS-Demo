using System;
using KUSYS_Demo.Data.Abstract;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class EfUnitOfWork : IUnitOfWork
    {
        private readonly KusysDbContext dbContext;
        public EfUnitOfWork(KusysDbContext _dbContext)
		{
            dbContext = _dbContext ?? throw new ArgumentNullException("KusysDbContext null olamaz");
        }

        private IUsersRepository _users;
        public IUsersRepository Users => _users ?? (_users = new EfUsersRepository(dbContext));

        private IStudentRepository _students;
        public IStudentRepository Students => _students ?? (_students = new EfStudentRepository(dbContext));

        private ICourseRepository _courses;
        public ICourseRepository Courses => _courses ?? (_courses = new EfCourseRepository(dbContext));

        private IMatchingsRepository _matchings;
        public IMatchingsRepository Matchings => _matchings ?? (_matchings = new EfMatchingsRepository(dbContext));

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

