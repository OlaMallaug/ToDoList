using ToDoListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        public ToDoList GetToDoList(int id)
        {
            return ToDoListContext.GetToDoList(id);
        }

        public ToDoList Create(ToDoList toDoList)
        {
            ToDoListContext.AddToDoList(toDoList);

            return toDoList;
        }
    }
}
