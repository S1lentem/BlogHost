using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class PostCreateModel
    {
        [Required(ErrorMessage = "Title is missing")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Tag is missing")]
        public string Title { get; set; }

        public string Tags { get; set; }
    }
}
