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
            dbSet = db.Set<ToDoList>(); // Назви ToDoLists
        }
        public async Task Add(ToDoList list)
        {
           dbSet.Add(list);
           await db.SaveChangesAsync();
        }

        public async Task<ToDoList?> Get(long key)
        {
            return await dbSet.FirstOrDefaultAsync(i=>i.Id == (int)key && i.IsDeleted == false); // пробіли навколо лямбди
        }

        public async Task<IEnumerable<ToDoList>> GetAll()
        {
            return await dbSet.Where(i=>i.IsDeleted==false).ToListAsync(); // пробіли навколо лямбди і ==
        }

        public async Task<bool> Remove(long key) //зайва опція, яка юзеру не потрібна
        {
            ToDoList? entity = await dbSet.FindAsync((int)key);
            if(entity != null)
            {
                db.Remove(entity);
                db.SaveChanges();
                return true;
            }
            return false;

         
            // переважно роблять так, щоб не ставити лишті дужки

            /*if (entity == null)
                return false;
            
            db.Remove(entity);
            db.SaveChanges();
            return true;*/
        }

        public async Task<bool> SoftRemove(long key) // SoftDelete, але видали оці звичайні деліти повністю, вони зайві, а цей перейменуй на звичайний деліт
        {
            var item = await dbSet.FindAsync((int)key);
            if(item.IsDeleted == false) // item може не знайтися і тут впаде ексепшн який ти ніде не ловиш
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
        }// строки пустої бракує
        public async Task<List<string>> GetNames() // ніде не використовується
        {
           return await dbSet.Select(x => x.Name).ToListAsync();
        }

        public async Task<bool> FindName(string name) //FindByName але по факту повертає бул тому правильне ім*я тут буде CheckIfNameExists
        {
            if (!await dbSet.AnyAsync(i=>i.Name == name)) // пробіли навколо лямбди
                return false; 
            return true;          
        }
    }
}
