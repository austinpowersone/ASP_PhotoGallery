
using System;
using System.ComponentModel.DataAnnotations;

namespace ASP_Photo_Gallery.Core
{
    public class Image : Entity<int>
    {
        [Display(Name = "Путь")]
        public string Path { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}