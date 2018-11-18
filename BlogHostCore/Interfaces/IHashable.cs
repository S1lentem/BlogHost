namespace BlogHostCore.Interfaces
{
    public interface IHashable
    {
        string GetHash();
        bool VerifyPassword(string haskToCheck);
        string Password { get; set; }
    }
}
