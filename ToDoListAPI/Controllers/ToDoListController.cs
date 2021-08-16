using ToDoListAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        [HttpGet("{id}")]
        public ToDoList GetToDoList(int id)
        {
            ToDoList toDoList = ToDoListContext.GetToDoList(id);

            return toDoList;
        }

        [HttpGet("All")]
        public IEnumerable<ToDoList> GetAllToDoLists()
        {
            var toDoList = ToDoListContext.GetAllToDoLists();

            return toDoList;
        }

        [HttpGet("AllAssociated/{id}")]
        public IEnumerable<ToDoList> GetAllAssociatedToDoLists(int id)
        {
            var toDoList = ToDoListContext.GetAllAssociatedToDoLists(id);

            return toDoList;
        }

        [HttpPost()]
        public string AddToDoList([FromBody] ToDoList toDoList)
        {
            ToDoListContext.AddToDoList(toDoList);
            return toDoList.Id.ToString();
        }

        [HttpDelete("{id}")]
        public string DeleteList(int id)
        {
            int result = ToDoListContext.DeleteList(id);

            return result.ToString();
        }

        [HttpPatch()]
        public string UpdateList(ToDoList toDoList)
        {
            int result = ToDoListContext.UpdateToDoList(toDoList);

            return result.ToString();
        }
    }
}
