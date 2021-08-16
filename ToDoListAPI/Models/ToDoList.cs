using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Models
{
    public class ToDoList : ToDoListContext
    {
        public int Id { get; set; }
        public string AssociatedWith { get; set; }
        public string Name { get; set; }
    }
       
}
