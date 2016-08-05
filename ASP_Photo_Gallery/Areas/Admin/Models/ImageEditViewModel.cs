
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ASP_Photo_Gallery.Areas.Admin.Models
{
    public class ImageEditViewModel
    {
        [Required]
        public int Id { get; set; }

        public HttpPostedFileBase File { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}