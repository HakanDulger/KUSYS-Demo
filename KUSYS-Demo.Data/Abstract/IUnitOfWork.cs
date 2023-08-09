using System;
namespace KUSYS_Demo.Data.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
        IUsersRepository Users { get; }
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IMatchingsRepository Matchings { get; }
    }
}

