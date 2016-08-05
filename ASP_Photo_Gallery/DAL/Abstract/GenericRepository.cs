using System.Data.Entity;
using System.Linq;

namespace ASP_Photo_Gallery.DAL.Abstract
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public void Insert(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }
    }
}