namespace Srp.Repositories
{
    public interface IRepository<T>
    {
        int Add(T item);
        List<T> GetAll();
        T GetById(int id);
        
    }
}