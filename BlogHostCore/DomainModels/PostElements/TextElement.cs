using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogHostCore.DomainModels.PostElements
{
    public class TextElement : PostElement
    {
        private string text;

        public TextElement(string text, int number) : base(number) 
            => this.text = text;

        public override string GetContentInfo() => text;
    }
}
