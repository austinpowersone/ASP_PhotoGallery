using System.Linq;

namespace ASP_Photo_Gallery.DAL.Abstract
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Query();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
