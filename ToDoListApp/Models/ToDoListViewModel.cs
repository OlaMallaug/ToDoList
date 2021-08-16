using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class ToDoListViewModel
    {
        public int id { get; set; }
        public string associatedWith { get; set; }
        public string name { get; set; }
    }

    public class ToDoListCompare : IEqualityComparer<ToDoListViewModel>
    {
        public bool Equals(ToDoListViewModel x, ToDoListViewModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.id == y.id && x.associatedWith == y.associatedWith && x.name == y.name;
        }

        public int GetHashCode([DisallowNull] ToDoListViewModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashId = obj.id.GetHashCode();
            int hashAssociatedWith = obj.associatedWith == null ? 0 : obj.associatedWith.GetHashCode();
            int hashName = obj.name == null ? 0 : obj.name.GetHashCode();

            return hashId ^ hashAssociatedWith ^ hashName;
        }
    }
}
