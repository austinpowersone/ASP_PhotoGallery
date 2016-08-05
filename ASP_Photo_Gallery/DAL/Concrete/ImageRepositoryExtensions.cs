using System.Linq;
using ASP_Photo_Gallery.Core;

namespace ASP_Photo_Gallery.DAL.Concrete
{
    public static class ImageRepositoryExtensions
    {
        public static Image Find(this IQueryable<Image> query, int id)
        {
            return query.FirstOrDefault(x => x.Id == id);
        }
    }
}