using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JustReadMe.ViewModels
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
