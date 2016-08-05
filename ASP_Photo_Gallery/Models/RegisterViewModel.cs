
using System.ComponentModel.DataAnnotations;

namespace ASP_Photo_Gallery.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Login")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}