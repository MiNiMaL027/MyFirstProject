using List_Domain.Models;

namespace List_Service.Interfaces
{
    internal interface IToDoTaskService<T> : IDefaultService<T> where T : class?
    {
        Task<List<T>> GetAllTaskByList(long id);
        Task<List<int>> SoftRemoveFew(List<int> item);
        //Task<List<T>> GetPlanned();
        //Task<List<T>> GetPlanned(int key);
        //Task<List<T>> GetImportant(int importanceValue);
        //Task<List<T>> GetImportant(int key,int importanceValue);
        //Task<List<T>> GetTodays();
        //Task<List<T>> GetTodays(int key);
        //Task<List<T>> GetFavorite();
        //Task<List<T>> GetFavorite(int key);
        //Task<List<T>> GetCompleted();
        Task<bool> CompleteOrUncompleteTask(int key);
        Task<List<ToDoTask>> GetAll(long key, string softListName, string orderName, bool orderType, int importantvalue);

    }
}
