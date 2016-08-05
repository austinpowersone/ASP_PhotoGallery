
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;

namespace ASP_Photo_Gallery.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        [DataType(DataType.Text)]
        public  string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}