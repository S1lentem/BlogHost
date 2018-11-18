using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nickname is not specified")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "No email address specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
