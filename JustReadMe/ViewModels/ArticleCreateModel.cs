using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.ViewModels
{
    public class ArticleCreateModel
    {
        [Required(ErrorMessage = "Title is missing")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Tag is missing")]
        public string Tag { get; set; }
    }
}
