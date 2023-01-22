using List_Domain.Models;

namespace List_Service.Interfaces
{
    public interface IDefaultService<T> where T : class?
    {
        Task<T> Get(long key);
        Task<List<T>> GetAll();
        Task<bool> Remove(long key);
        Task<bool> Update(T item);
        Task<bool> SoftRemove(long key);
        Task Add(T item);
    }
}
