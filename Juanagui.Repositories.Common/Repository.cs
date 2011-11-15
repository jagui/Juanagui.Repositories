using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juanagui.Repositories.Common
{
    public abstract class Repository<T> : IDisposable
        where T : class
    {
        public abstract void Dispose();
        public abstract IQueryable<T> Query();
        public abstract IEnumerable<T> All();
        public abstract void Add(T entity);
        public abstract void Attach(T entity);
        public abstract void Delete(T entity);
        public abstract void PersistAll();
    }
}
