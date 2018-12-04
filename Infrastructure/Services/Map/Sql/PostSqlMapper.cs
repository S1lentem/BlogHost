using BlogHostCore.DomainModels;
using BlogHostCore.DomainModels.PostElements;
using Infrastructure.Models;
using Infrastructure.Models.PostComponents;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services.Map.Sql
{
    public class PostSqlMapper
    {
        public Post GetDomain(PostModel model, IEnumerable<TextComponent> texts, IEnumerable<ImageComponent> images, 
            IEnumerable<CommentModel> comments = default(IEnumerable<CommentModel>))
        {
            var commentMapper = new CommentSqlMapper();

            IEnumerable<PostElement> textElements = texts?.Select(textModel => new TextElement(textModel.Text, textModel.OrderNum));
            IEnumerable<PostElement> imageElements = images?.Select(imageModel => new ImageElement(imageModel.ImagePath, imageModel.OrderNum));
            IEnumerable<PostElement> postElements;
            if (imageElements != null) postElements = textElements?.Concat(imageElements).ToList();
            else postElements = textElements;

            return new Post(
                model.Id, 
                model.Title,
                postElements?.OrderBy(item => item.Number).ToList(),
                model.DateCreation, 
                model.DateChange, 
                model.BlogModel.UserModel.Nickname, 
                comments?.Select(comment => commentMapper.GetDomain(comment)).ToList(), 
                model.PostTag.Select(tag => tag.Tag.Title).ToList());
        }
    }
}
