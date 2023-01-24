namespace List_Dal.Interfaces
{
    public interface IToDoListRepository<T> : IDefaultRepository<T> where T : class?
    {
        Task<List<string>> GetNames();

        Task<bool> CheckIfNameExist(string name);

    }
}
