using System;
using System.Collections.Generic;
using System.Data.Entity;
using ASP_Photo_Gallery.DAL.Abstract;
using ASP_Photo_Gallery.DAL.Context;

namespace ASP_Photo_Gallery.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context = new ApplicationDbContext();
        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public void Dispose()
        {
            _context?.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }
            var repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(type, repository);
            return repository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}