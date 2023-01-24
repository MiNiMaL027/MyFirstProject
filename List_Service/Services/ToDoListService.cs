using List_Dal.Interfaces;
using List_Domain.Models;
using List_Service.Interfaces;
using List_Service.Services.ValidServices;

namespace List_Service.Services
{
    public class ToDoListService : IToDoListService<ToDoList>
    {
        private readonly IToDoListRepository<ToDoList> _repository;
        public ToDoListService(IToDoListRepository<ToDoList> repository)
        {
            _repository = repository;
        }

        public async Task Add(ToDoList list)
        {
            if (await _repository.CheckIfNameExist(list.Name))
                throw new Exception("This name you used");
            list.Name = list.Name.Trim();
            if (!ValidOptions.ValidName(list.Name))
                throw new Exception("The name is not correct");    
            await _repository.Add(list);         
        }

        public async Task<ToDoList> Get(long key)
        {
            return await _repository.Get(key);
        }

        public async Task<List<ToDoList>> GetAll()
        {
            return (List<ToDoList>)await _repository.GetAll();
        }

        public async Task<bool> Update(ToDoList list)
        {
           return await _repository.Update(list);
        }

        public async Task<bool> Remove(long key)
        {
            return await _repository.Remove(key);
        }
    }
}
