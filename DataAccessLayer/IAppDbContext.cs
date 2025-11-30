using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Èםעונפויס הכÿ DbContext
    /// </summary>
    public interface IAppDbContext
    {
        Microsoft.EntityFrameworkCore.DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
