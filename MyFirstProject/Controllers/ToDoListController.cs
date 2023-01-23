using List_Domain.Models;
using List_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstProject_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : Controller
    {
        public ToDoListService service { get; set; } // приватна рідонла змінна 
        public ToDoListController(ToDoListService service) // інтерфес
        {
            this.service = service;
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<ToDoList>>> GetList() 
        {
            var c = Ok(await service.GetAll()); // в чому прикол? просто return OK....
            return c;
        }

        [HttpGet("Alone")]
        public async Task<ActionResult<ToDoList>> Get(long id) // інт
        {
            return Ok(await service.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Add(ToDoList list) 
        {
            await service.Add(list);
            return Ok(list.Id);
        }

        [HttpDelete("DataDelete")]
        public async Task<ActionResult<int>> Remove(long id) // інт
        {
            if(await service.Remove(id))
             return Ok(id);
            return NotFound(id);
        }

        [HttpDelete("SoftDelete")]
        public async Task<ActionResult<int>> SoftRemove(long id) // інт
        {
            if(await service.SoftRemove(id))
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
