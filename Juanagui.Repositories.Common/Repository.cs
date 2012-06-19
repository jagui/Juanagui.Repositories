using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juanagui.Repositories.Common
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
// ReSharper disable ReturnTypeCanBeEnumerable.Global
        IQueryable<T> Query();
// ReSharper restore ReturnTypeCanBeEnumerable.Global
        IEnumerable<T> All();
        void Add(T entity);
        void Attach(T entity);
        void Delete(T entity);
        void PersistAll();
    }
}
