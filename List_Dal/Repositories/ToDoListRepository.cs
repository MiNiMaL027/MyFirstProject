using List_Dal.Interfaces;
using List_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace List_Dal.Repositories
{
    public class ToDoListRepository : IToDoListRepository<ToDoList>
    {
        ApplicationContext db;
        DbSet<ToDoList> dbSet;
        public ToDoListRepository(ApplicationContext context)
        {
            db = context;
            dbSet = db.Set<ToDoList>();
        }
        public async Task<int> Add(ToDoList list)
        {
           dbSet.Add(list);
           await db.SaveChangesAsync();
           return list.Id;
        }

        public async Task<ToDoList?> Get(long key)
        {
            return await dbSet.FirstOrDefaultAsync(i => i.Id == (int)key && i.IsDeleted == false);
        }

        public async Task<IEnumerable<ToDoList>> GetAll()
        {
            return await dbSet.Where(i=>i.IsDeleted==false).ToListAsync();
        }

        public async Task<bool> Remove(long key)
        {
            var item = await dbSet.FindAsync((int)key);
            if(item.IsDeleted == false)
            {
                item.IsDeleted = true;
                db.SaveChanges();
            }
            return item.IsDeleted;
        }

        public async Task<bool> Update(ToDoList list)
        {
            if (dbSet.Contains(list) && list.IsDeleted == false)
            {
                db.Update(list);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<string>> GetNames()
        {
           return await dbSet.Select(x => x.Name).ToListAsync();
        }

        public async Task<bool> CheckIfNameExist(string name)
        {
            if (!await dbSet.AnyAsync(i => i.Name == name))
                return false;
            return true;          
        }
    }
}
