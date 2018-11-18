namespace BlogHostCore.Interfaces.Repository
{
    public interface IBaseRepository<T>
        where T : class
    {
        T GetById(int id);
    }
}