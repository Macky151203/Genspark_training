namespace Srp.Repositories
{
    
    public class Repository<T> : IRepository<T>
    {
        private List<T> _items = new List<T>();
        private int _nextId = 1;

        public int Add(T item)
        {
            _items.Add(item);
            return _nextId++;
        }

        public List<T> GetAll()
        {
            return _items;
        }

        public T GetById(int id)
        {
            return _items[id];
        }
    }
}