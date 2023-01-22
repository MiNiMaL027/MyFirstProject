using List_Domain.Models;
using List_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstProject_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : Controller
    {
        public ToDoTaskService service { get; set; }
        public ToDoTaskController(ToDoTaskService service)
        {
            this.service = service;
        }

        #region Gets
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ToDoTask>> Get(long id)
        {
            return Ok(await service.Get(id));
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<ToDoTask>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("AllByList/{listId:int}")]
        public async Task<ActionResult<List<ToDoTask>>> GetAll(int listId)
        {
            return Ok(await service.GetAllTaskByList(listId));
        }

        [HttpGet("AlLOrder")]
        public async Task<ActionResult<List<ToDoTask>>> GetAll(long? listId,string? softListName,string? orderName,bool orderType,int? importantValue)
        {
            return Ok(await service.GetAll(listId, softListName, orderName, orderType, importantValue));
        }

        #region CustomGets
        //[HttpGet("Planned")]
        //public async Task<ActionResult<List<ToDoTask>>> GetPlaned()
        //{
        //    return Ok(await service.GetPlanned());
        //}

        //[HttpGet("Planned/{listId:int}")]
        //public async Task<ActionResult<List<ToDoTask>>> GetPlaned(int listId)
        //{
        //    return Ok(await service.GetPlanned(listId));
        //}

        //[HttpGet("Importance")]
        //public async Task<ActionResult<List<ToDoTask>>> GetImportance(int importanceValue)
        //{
        //    return Ok(await service.GetImportant(importanceValue));
        //}

        //[HttpGet("Importance/{listId:int}")]
        //public async Task<ActionResult<List<ToDoTask>>> GetImportance(int listId, int importanceValue)
        //{
        //    return Ok(await service.GetImportant(listId, importanceValue));
        //}

        //[HttpGet("Today")]
        //public async Task<ActionResult<List<ToDoTask>>> GetToday()
        //{
        //    return Ok(await service.GetTodays());
        //}


        //[HttpGet("Today/{listId:int}")]
        //public async Task<ActionResult<List<ToDoTask>>> GetToday(int listId)
        //{
        //    return Ok(await service.GetTodays(listId));
        //}

        //[HttpGet("Favorite")]
        //public async Task<ActionResult<List<ToDoList>>> GetFavorite()
        //{
        //    return Ok(await service.GetFavorite());
        //}

        //[HttpGet("Favorite{listId:int}")]
        //public async Task<ActionResult<List<ToDoList>>> GetFavorite(int listId)
        //{
        //    return Ok(await service.GetFavorite(listId));
        //}

        //[HttpGet("Complited")]
        //public async Task<ActionResult<List<ToDoList>>> GetComplited()
        //{
        //    return Ok(await service.GetCompleted());
        //}
        #endregion
        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Add(ToDoTask task)
        {
            await service.Add(task);
            return Ok(task.Id);
        }

        [HttpDelete("SoftDelete")]
        public async Task<ActionResult<int>> SoftDelete(long id)
        {
            await service.SoftRemove(id);
            return Ok(id);
        }

        [HttpDelete("DataDelete")]
        public async Task<ActionResult<int>> Delete(long id)
        {
            await service.Remove(id);
            return Ok(id);
        }

        [HttpDelete("SoftFewDelete")]
        public async Task<ActionResult<List<int>>> FewSoftDelete(List<int> ids)
        {
            await service.SoftRemoveFew(ids);
            return Ok(ids);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(ToDoTask task)
        {
            await service.Update(task);
            return task.Id;
        }
    }
}
