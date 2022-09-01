using System.ComponentModel.DataAnnotations;
using Minstrels.Enum;

namespace Minstrels.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        public MinstrelType MinstrelType { get; set; }
        public string  Description { get; set; }

    }
}
