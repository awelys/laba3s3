using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using laba1s3core;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        private readonly AppDbContext _context;
        public EntityRepository(AppDbContext context)
        {
            _context = context;
            //_context.Database.EnsureCreated();
            //DbSeeder.Seed(_context);
        }
        public void Add(T entity) { _context.Set<T>().Add(entity); _context.SaveChanges(); }
        public bool Remove(int id) { var set = _context.Set<T>(); var e = set.Find(id); if (e==null) return false; set.Remove(e); _context.SaveChanges(); return true; }
        public T? Get(int id) => _context.Set<T>().Find(id);
        public IEnumerable<T> GetAll() => _context.Set<T>().AsNoTracking().ToList();
        public bool Update(T entity) { var set = _context.Set<T>(); var exist = set.Find(entity.Id); if (exist==null) return false; _context.Entry(exist).CurrentValues.SetValues(entity); _context.SaveChanges(); return true; }
    }
}
