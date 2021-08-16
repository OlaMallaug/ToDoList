using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public interface IToDoListRepository
    {
        ToDoList GetToDoList(int id);
        ToDoList Create(ToDoList toDoList);
    }
}
