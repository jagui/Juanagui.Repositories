using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Juanagui.Repositories.Common;
using System.Data.Metadata.Edm;
using System.Data.Entity;

namespace Juanagui.Repositories.EntityFramework
{
    public class EntityFrameworkPocoRepository<T> : Repository<T> where T : class
    {
        private readonly DbContext _db;

        public EntityFrameworkPocoRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _db = dbContext;
        }

        public IQueryable<T> Query()
        {
            return _db.Set<T>();
        }
        public IEnumerable<T> All()
        {
            return Query().AsEnumerable();
        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        public void Attach(T entity)
        {
            _db.Set<T>().Attach(entity);
        }
        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public void PersistAll()
        {
            _db.SaveChanges();
        }
        public void Dispose()
        {
            PersistAll();
            _db.Dispose();
        }
    }
}
