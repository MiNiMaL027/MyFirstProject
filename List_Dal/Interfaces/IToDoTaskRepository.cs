using List_Domain.Models;

namespace List_Dal.Interfaces
{
    public interface IToDoTaskRepository<T> : IDefaultRepository<T> where T : class
    {
        Task<bool> FindName(string name); // FindByName

        Task<List<T>> GetAllTaskByList(long key);
        Task<List<int>> SoftRemoveFew(List<int> item);
        //Task<List<T>> GetPlanned(int key);
        //Task<List<T>> GetPlanned();
        //Task<List<T>> GetImportant(int key,int importanceValue);
        //Task<List<T>> GetImportant(int importanceValue);
        //Task<List<T>> GetTodays(int key);
        //Task<List<T>> GetTodays();
        //Task<List<T>> GetFavorite(int key);
        //Task<List<T>> GetFavorite();    
        //Task<List<T>> GetCompleted();
        Task<bool> CompleteOrUncompleteTask(int key);
        Task<List<ToDoTask>> GetAll(long? key, string? softListName, string? orderName, bool orderType, int? importantvalue);
    }
}
