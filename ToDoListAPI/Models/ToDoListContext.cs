using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ToDoListAPI.Models
{
    public class ToDoListContext
    {
        private static string ConnectionString = "Data Source=:memory:;Version=3;New=True;cache=shared";
        private static SQLiteConnection sQLiteConnection = new SQLiteConnection(ConnectionString);
        

        public static void CreateTables()
        {
            sQLiteConnection.Open();

            string query = "create table ToDoList (Id INTEGER PRIMARY KEY, AssociatedWith varchar(50), Name varchar(50))";
            using (SQLiteCommand cmd = new SQLiteCommand(query, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }

            query = "create table ToDoListItem (Id INTEGER PRIMARY KEY, ListId int, Description varchar(50), Completed Bool)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void AddToDoList(ToDoList toDoList)
        {
            //TODO: Check if name exists

            string sql = "insert into ToDoList (AssociatedWith, Name) values ('" + toDoList.AssociatedWith + "','" + toDoList.Name + "')";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static ToDoList GetToDoList(int id)
        {
            ToDoList toDoLists = new ToDoList();

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select * from ToDoList where id=" + id;
                cmd.CommandType = CommandType.Text;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    toDoLists.Id = Convert.ToInt32(reader["Id"]);
                    toDoLists.AssociatedWith = reader["AssociatedWith"].ToString();
                    toDoLists.Name = reader["Name"].ToString();
                }

            }
            
            return toDoLists;
        }

        public static IEnumerable<ToDoList> GetAllToDoLists()
        {
            List<ToDoList> toDoLists = new List<ToDoList>();

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select * from ToDoList";
                cmd.CommandType = CommandType.Text;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ToDoList currentToDoList = new ToDoList();
                    currentToDoList.Id = Convert.ToInt32(reader["Id"]);
                    currentToDoList.AssociatedWith = reader["AssociatedWith"].ToString();
                    currentToDoList.Name = reader["Name"].ToString();

                    toDoLists.Add(currentToDoList);
                }

            }

            return toDoLists;
        }

        public static IEnumerable<ToDoList> GetAllAssociatedToDoLists(int id)
        {
            string associatedWith = "";

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select AssociatedWith from ToDoList where id=" + id;
                cmd.CommandType = CommandType.Text;

                associatedWith = cmd.ExecuteScalar().ToString();
            }

            List<ToDoList> toDoLists = new List<ToDoList>();

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select * from ToDoList where AssociatedWith='" + associatedWith + "'";
                cmd.CommandType = CommandType.Text;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ToDoList currentToDoList = new ToDoList();
                    currentToDoList.Id = Convert.ToInt32(reader["Id"]);
                    currentToDoList.AssociatedWith = reader["AssociatedWith"].ToString();
                    currentToDoList.Name = reader["Name"].ToString();

                    toDoLists.Add(currentToDoList);
                }

            }

            return toDoLists;
        }

        public static void AddToDoListItem(ToDoListItem toDoListItem)
        {
            string sql = "insert into ToDoListItem (ListId , Description , Completed) values ('" + toDoListItem.ListId + "','" + toDoListItem.Description + "','" + toDoListItem.Completed + "')";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static ToDoListItem GetToDoListItem(int id)
        {
            ToDoListItem toDoListItem = new ToDoListItem();

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select * from ToDoListItem where id=" + id;
                cmd.CommandType = CommandType.Text;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    toDoListItem.Id = Convert.ToInt32(reader["Id"]);
                    toDoListItem.ListId = Convert.ToInt32(reader["ListId"]);
                    toDoListItem.Description = reader["Description"].ToString();
                    toDoListItem.Completed = Convert.ToBoolean(reader["Completed"]);

                }
            }

            return toDoListItem;
        }

        public static IEnumerable<ToDoListItem> GetAllToDoListItems(int id)
        {
            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();

            using (SQLiteCommand cmd = sQLiteConnection.CreateCommand())
            {
                cmd.CommandText = "select * from ToDoListItem where ListId=" + id;
                cmd.CommandType = CommandType.Text;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ToDoListItem currentToDoListItem = new ToDoListItem();
                    currentToDoListItem.Id = Convert.ToInt32(reader["Id"]);
                    currentToDoListItem.ListId = Convert.ToInt32(reader["ListId"]);
                    currentToDoListItem.Description = reader["Description"].ToString();
                    currentToDoListItem.Completed = Convert.ToBoolean(reader["Completed"]);

                    toDoListItems.Add(currentToDoListItem);
                }
            }

            return toDoListItems;
        }

        public static int MarkToDoItemCompleted(int id)
        {
            try
            {
                string sql = "update ToDoListItem set Completed = true where Id=" + id;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        public static int DeleteItem(int id)
        {
            try
            {
                string sql = "delete from ToDoListItem where Id=" + id;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public static int DeleteList(int id)
        {
            try
            {
                string sql = "delete from ToDoList where Id=" + id;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
                {
                    cmd.ExecuteNonQuery();
                }

                sql = "delete from ToDoListItem where ListId=" + id;
                using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        public static int UpdateToDoItem(ToDoListItem toDoListItem)
        {
            try 
            { 
            string sql = "update ToDoListItem set Description='" + toDoListItem.Description + "' where id=" + toDoListItem.Id;
            using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        public static int UpdateToDoList(ToDoList toDoList)
        {
            try
            { 
            string sql = "update ToDoList set Name='" + toDoList.Name + "', AssociatedWith='" + toDoList.AssociatedWith + "' where id=" + toDoList.Id;
            using (SQLiteCommand cmd = new SQLiteCommand(sql, sQLiteConnection))
            {
                cmd.ExecuteNonQuery();
            }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
    }

}
