﻿using List_Dal.Interfaces;
using List_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace List_Dal.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository<ToDoTask>
    {
        ApplicationContext db;
        DbSet<ToDoTask> dbSet;
        public ToDoTaskRepository(ApplicationContext context)
        {
            db = context;
            dbSet = db.Set<ToDoTask>(); // ToDoTasks 
        }

        public async Task Add(ToDoTask task) //  має повертати айді
        {
            dbSet.Add(task);// Адд повертає ентетю з бази яку ти не ловиш, хапаєш айді і передаєш його на ендпойнт
            await db.SaveChangesAsync();
        }
        /// <summary> 
        /// will check if there is transferred to whether it meets the norms 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True or False</returns>
        public async Task<bool> FindName(string name) // CheckIfNameExists, названо всюди нейм, а шукаєш тайтл
        {
            if (!await dbSet.AnyAsync(i => i.Title == name))
                return false;
            return true;
        }

        public async Task<ToDoTask?> Get(long key) // GetById, для чого тут лонг якщо воно інт? витрачаєш лишню оперативку
        {
            return await dbSet.FirstOrDefaultAsync(i => i.Id == (int)key && i.IsDeleted == false);
        }

        public async Task<IEnumerable<ToDoTask?>> GetAll()
        {
            return await dbSet.Where(i => i.IsDeleted == false).ToListAsync(); //  i.IsDeleted == false це те саме що !i.IsDeleted, якщо змінна сама по саобі булл то її нема сенсу порівнювати
        }

        public async Task<bool> Remove(long key) // Лонг тут не потрібен, зроби інт
        {
            ToDoTask? entity = await dbSet.FindAsync((int)key);
            if (entity != null)
            {
                db.Remove(entity);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> SoftRemove(long key) // Не лонг а інт
        {
            var item = await dbSet.FindAsync((int)key);
            if (item.IsDeleted == false) // Айтем може буи NULL впаде ексепшн який ти не ловиш
            {
                item.IsDeleted = true;
                db.SaveChanges();
            }
            return item.IsDeleted;
        }

        public async Task<bool> Update(ToDoTask task)
        {
            if(dbSet.Contains(task) && task.IsDeleted == false) // !task.IsDeleted, dbSet.Contains(task) тут має бути гет бай айді і перевірка на null, я не вірю що він працює
            {
                dbSet.Update(task);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ToDoTask>> GetAllTaskByList(long key) // інт
        {
           return await dbSet.Where(t=>t.ToDoListId == key && t.IsDeleted==false).ToListAsync(); // пробіли і не треба умови на IsDeleted
        }

        public async Task<List<ToDoTask>> GetAll(long? key,string? softListName,string? orderName,bool orderType,int? importantvalue)// пробіли після ком,
 // ну от і приїхали) ти на це напевне в тричі більше часу потратив ніж вивчив би що таке OData, цей код треш, уявлю що я його не бачив))так не робиться       
        {           
            var list = dbSet.Where(t => t.IsDeleted == false);
            switch (softListName)
            {
                case "Planned":
                    list = list.Where(i => i.DueData != null);
                    break;
                case "Today":
                    list = list.Where(i => i.DueData == DateTime.Now.Date);
                    break;
                case "Important":
                    list = list.Where(i => i.Importance == importantvalue);
                    break;
                case "Favorite":
                    list = list.Where(i => i.IsFavorites == true); // не треба умови на інт
                    break;
                case "Completed":
                    list = list.Where(i => i.IsCompleted == true); // не треба умови на інт
                    break;
                default:
                    list = list.Where(i => i.ToDoListId == key);
                    break;
            }
            if (orderType)
            {
                switch (orderName)
                {
                    case "Name":
                        list = list.OrderBy(l => l.Title);
                        break;
                    case "DueDate":
                        list = list.OrderBy(l => l.DueData);
                        break;
                    case "DateCreate":
                        list = list.OrderBy(l => l.Id);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (orderName)
                {
                    case "Name":
                        list = list.OrderByDescending(l => l.Title);
                        break;
                    case "DueDate":
                        list = list.OrderByDescending(l => l.DueData);
                        break;
                    case "DateCreate":
                        list = list.OrderByDescending(l => l.Id);
                        break;
                    default:
                        break;
                }
            }
            var listResult = await list.ToListAsync();
            return listResult;

        }
        public async Task<List<int>> SoftRemoveFew(List<int> keys) // DeleteMultiple, Ids
        {
            for (int i = 0; i < keys.Count; i++)
            {
                var item = await dbSet.FindAsync(keys[i]);
                if (item.IsDeleted == false) // не порівнюй бул і айтем може бути нуль, ексепшн не хендлиш
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                }
                else                  
                    throw new Exception($"{item.Id} - Is deleted");              
            }
            return keys;

        }
        #region CustomGets
        //public async Task<List<ToDoTask>> GetPlanned(int listid)
        //{
        //    return await dbSet.Where(i => i.IsDeleted == false && i.DueData != null && i.ToDoListId == listid).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetImportant(int listid,int importancevalue)
        //{
        //    return await dbSet.Where(i => i.IsDeleted == false && i.Importance == importancevalue && i.ToDoListId == listid).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetImportant(int importancevalue)
        //{
        //    return await dbSet.Where(i => i.IsDeleted == false && i.Importance == importancevalue).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetTodays(int listid)
        //{
        //    DateTime? currentDataTime = DateTime.Today;
        //    return await dbSet.Where(i => i.IsDeleted == false && i.DueData == currentDataTime && i.ToDoListId == listid).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetTodays()
        //{
        //    DateTime? currentDataTime = DateTime.Today;
        //    return await dbSet.Where(i => i.IsDeleted == false && i.DueData == currentDataTime).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetFavorite(int listid)
        //{
        //    return await dbSet.Where(i => i.IsFavorites == true && i.IsDeleted == false && i.ToDoListId == listid).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetFavorite()
        //{
        //    return await dbSet.Where(i => i.IsFavorites == true && i.IsDeleted == false).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetPlanned()
        //{
        //    return await dbSet.Where(i => i.IsDeleted == false && i.DueData != null).ToListAsync();
        //}

        //public async Task<List<ToDoTask>> GetCompleted()
        //{
        //    return await dbSet.Where(i=>i.IsCompleted == true).ToListAsync();
        //}
        #endregion
        public async Task<bool> CompleteOrUncompleteTask(int key)// Id
        {
            var item = await dbSet.FindAsync(key);
            if(item.IsCompleted == false) // то саме, ексепшн якщо не знайде
            { 
                item.IsCompleted= true;              
            }
            else
            {
                item.IsCompleted= false;
            }
            return item.IsCompleted;              
        }

    }

}
