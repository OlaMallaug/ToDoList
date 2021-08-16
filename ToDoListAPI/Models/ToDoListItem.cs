using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Models
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
