using System.ComponentModel.DataAnnotations;
namespace ASP_Photo_Gallery.Core
{
    public abstract class Entity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}