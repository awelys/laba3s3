using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using laba1s3core;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для Entity Framework репозитория
    /// </summary>
    public interface IEntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
    }

    public class EntityRepository<T> : IEntityRepository<T> where T : class, IDomainObject, new()
    {
        private readonly AppDbContext _context;
        
        public EntityRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public bool Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            var set = _context.Set<T>();
            var entity = set.Find(id);
            
            if (entity == null)
                return false;

            set.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public T? Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(id));

            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public bool Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException("ID должен быть больше 0", nameof(entity.Id));

            var set = _context.Set<T>();
            var existing = set.Find(entity.Id);
            
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
