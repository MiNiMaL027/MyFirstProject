using List_Dal.Interfaces;
using List_Domain.Models;
using List_Service.Interfaces;
using List_Service.Services.ValidServices;

namespace List_Service.Services
{
    public class ToDoTaskService : IToDoTaskService<ToDoTask>
    {
        private readonly IToDoTaskRepository<ToDoTask> _repository;
        public ToDoTaskService(IToDoTaskRepository<ToDoTask> repository)
        {
            _repository = repository;
        }

        public async Task Add(ToDoTask item)
        {
            if (await _repository.FindName(item.Title))
                throw new Exception("This name you used");
            item.Title = item.Title.Trim();
            if (!ValidOptions.ValidName(item.Title))
                throw new Exception("The name is not correct");
            if (!ValidOptions.ValidImportance(item.Importance))
                throw new Exception("Uncorrect importance");
            await _repository.Add(item);
        }

        public Task<bool> CompleteOrUncompleteTask(int key)
        {
            return _repository.CompleteOrUncompleteTask(key);
        }
        #region Gets
        public async Task<ToDoTask> Get(long key)
        {
            return await _repository.Get(key);
        }

        public async Task<List<ToDoTask>> GetAll()
        {
            return (List<ToDoTask>)await _repository.GetAll();
        }

        public async Task<List<ToDoTask>> GetAllTaskByList(long id)
        {
            return await _repository.GetAllTaskByList((int)id);
        }

        public async Task<List<ToDoTask>> GetAll(long key, string softListName, string orderName, bool orderType, int importantvalue)
        {
            if (!ValidOptions.ValidImportance(importantvalue))
                throw new Exception("Uncorrect importance");
            return await _repository.GetAll(key, softListName, orderName, orderType, importantvalue);
        }

        //public async Task<List<ToDoTask>> GetFavorite()
        //{
        //    return await _repository.GetFavorite();
        //}

        //public async Task<List<ToDoTask>> GetFavorite(int listId)
        //{
        //    return await (_repository.GetFavorite(listId));
        //}

        //public async Task<List<ToDoTask>> GetImportant(int importanceValue)
        //{
        //    if (!ValidOptions.ValidImportance(importanceValue))
        //        throw new Exception("Uncorrect importance");
        //    return await _repository.GetImportant(importanceValue);
        //}

        //public async Task<List<ToDoTask>> GetImportant(int listId, int importanceValue)
        //{
        //    if (!ValidOptions.ValidImportance(importanceValue))
        //        throw new Exception("Uncorrect importance");
        //    return await _repository.GetImportant(listId,importanceValue);
        //}

        //public async Task<List<ToDoTask>> GetPlanned()
        //{
        //    return await _repository.GetPlanned();
        //}

        //public async Task<List<ToDoTask>> GetPlanned(int listId)
        //{
        //    return await _repository.GetPlanned(listId);
        //}

        //public async Task<List<ToDoTask>> GetTodays()
        //{
        //    return await _repository.GetTodays();
        //}

        //public async Task<List<ToDoTask>> GetTodays(int listId)
        //{
        //    return await _repository.GetTodays(listId);
        //}

        //public async Task<List<ToDoTask>> GetCompleted()
        //{
        //    return await _repository.GetCompleted();
        //}
        #endregion
        public async Task<bool> Remove(long key)
        {
            return await _repository.Remove(key);
        }

        public async Task<bool> SoftRemove(long key)
        {
            return await(_repository.SoftRemove(key));
        }

        public async Task<List<int>> SoftRemoveFew(List<int> item)
        {
            return await _repository.SoftRemoveFew(item);
        }

        public async Task<bool> Update(ToDoTask task)
        {
            return await _repository.Update(task);
        }
    }
}
