namespace BlogHostCore.DomainModels.PostElements
{
    public abstract class PostElement
    {
        public int Number { get; }

        public PostElement(int number) => this.Number = number;

        public abstract string GetContentInfo();
    }
}
