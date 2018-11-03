using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace JustReadMe.TagHelpers.bootstrap
{
    public class MenuComponentTagHelper : TagHelper
    {
        private readonly string attributeValue = "col-lg-4";
        private readonly string attributeName = "class";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.SetAttribute(attributeName, attributeValue);
        }
    }
}
