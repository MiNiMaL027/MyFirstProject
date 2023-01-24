using List_Domain.Models;
using List_Service.Services;
using Microsoft.AspNetCore.Mvc;
using List_Service.Interfaces;

namespace MyFirstProject_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : Controller
    {
        private readonly IToDoListService<ToDoList> service;
        public ToDoListController(IToDoListService<ToDoList> service)
        {
            this.service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<ToDoList>>> GetList() 
        {        
            return Ok(await service.GetAll());
        }

        [HttpGet("Alone")]
        public async Task<ActionResult<ToDoList>> Get(long id)
        {
            return Ok(await service.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(ToDoList list) 
        {
            await service.Add(list);
            return Ok(list.Id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Remove(long id)
        {
            if(await service.Remove(id))
               return Ok(id);
            return NotFound(id);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(ToDoList list)
        {
            if(await service.Update(list))
                return Ok(list.Id);
            return NotFound(list.Id);
        }

    }
}
