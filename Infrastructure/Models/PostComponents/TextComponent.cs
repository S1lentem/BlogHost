using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.PostComponents
{
    public class TextComponent
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int OrderNum { get; set; }

        public int PostId { get; set; }
        public virtual PostModel Post { get; set; }
    }
}
