using System.IO;

namespace BlogHostCore.DomainModels.PostElements
{
    public class ImageElement : PostElement
    {
        private readonly string pathToImage;

        public ImageElement(string pathToImage, int number) : base(number)
            => this.pathToImage = pathToImage;

        public override string GetContentInfo() => pathToImage;
    }
}
