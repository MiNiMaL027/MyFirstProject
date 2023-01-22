namespace List_Dal.Interfaces
{
    public interface IDefaultRepository<T> where T : class?
    {
        Task Add(T list);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(long key);
        Task<bool> Remove(long key);
        Task<bool> Update(T list);
        Task <bool> SoftRemove(long key);
    }
}
