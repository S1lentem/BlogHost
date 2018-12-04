namespace BlogHostCore.DomainModels.PostElements
{
    public class PostElementFactory
    {
        private int currentNumber = 0;

        public PostElement CreateText(string text) => new TextElement(text, currentNumber++);

        public PostElement CreateImage(string pathToImage) => new ImageElement(pathToImage, currentNumber++);

        public void ResetNumber() => currentNumber = 0;
    }
}