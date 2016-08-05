
using System;

namespace ASP_Photo_Gallery.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
}
