using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is missing")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwords is missing")]
        [DataType(DataType.Password)]
        public string Passwords { get; set; }
    }
}
