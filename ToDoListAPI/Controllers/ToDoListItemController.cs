using ToDoListAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListItemController : Controller
    {
        [HttpGet("List/{id}")]
        public IEnumerable<ToDoListItem> GetToDoListItems(int id)
        {
            var toDoList = ToDoListContext.GetAllToDoListItems(id);

            return toDoList;
        }

        [HttpGet("{id}")]
        public ToDoListItem GetToDoListItem(int id)
        {
            var toDoListItem = ToDoListContext.GetToDoListItem(id);

            return toDoListItem;
        }

        [HttpPost()]
        public string AddToDoListItem([FromBody] ToDoListItem toDoListItem)
        {
            ToDoListContext.AddToDoListItem(toDoListItem);

            return toDoListItem.Id.ToString();
        }

        [HttpPatch("MarkCompleted/{id}")]
        public string MarkToDoItemCompleted(int id)
        {
            int result = ToDoListContext.MarkToDoItemCompleted(id);

            return result.ToString();
        }


        [HttpDelete("{id}")]
        public string DeleteItem(int id)
        {
            int result = ToDoListContext.DeleteItem(id);

            return result.ToString();
        }

        [HttpPatch()]
        public string UpdateItem(ToDoListItem toDoListItem)
        {
            int result = ToDoListContext.UpdateToDoItem(toDoListItem);

            return result.ToString();
        }


    }
}
