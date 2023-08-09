using System;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Entity;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class EfMatchingsRepository : EfGenericRepository<Matchings>, IMatchingsRepository
    {
        public EfMatchingsRepository(DbContext _context) : base(_context)
        {
        }
        public KusysDbContext KusysDbContext
        {
            get { return context as KusysDbContext; }
        }
    }
}

