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

        protected EntityFrameworkPocoRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _db = dbContext;
        }

        public override IQueryable<T> Query()
        {
            return _db.Set<T>();
        }
        public override IEnumerable<T> All()
        {
            return Query().AsEnumerable();
        }
        public override void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        public override void Attach(T entity)
        {
            _db.Set<T>().Attach(entity);
        }
        public override void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        public override void PersistAll()
        {
            _db.SaveChanges();
        }
        public override void Dispose()
        {
            _db.Dispose();
        }
    }
}
