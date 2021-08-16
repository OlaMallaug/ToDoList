using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class ToDoListItemViewModel
    {
        public int id { get; set; }
        public int listId { get; set; }
        public string description { get; set; }
        public bool completed { get; set; }
    }

    public class ToDoListItemCompare : IEqualityComparer<ToDoListItemViewModel>
    {
        public bool Equals(ToDoListItemViewModel x, ToDoListItemViewModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.id == y.id && x.listId == y.listId && x.description == y.description && x.completed == y.completed;
        }

        public int GetHashCode([DisallowNull] ToDoListItemViewModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashDescription = obj.description == null ? 0 : obj.description.GetHashCode();
            int hashId = obj.id.GetHashCode();
            int hashListId = obj.listId.GetHashCode();
            
            return hashDescription ^ hashId ^ hashListId;
        }
    }
}
