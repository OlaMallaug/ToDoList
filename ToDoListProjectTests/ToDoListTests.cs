using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using ToDoListApp.Models;

namespace ToDoListProjectTests
{
    [TestClass]
    public class ToDoListTests
    {
        static List<ToDoListViewModel> expectedToDoLists = new List<ToDoListViewModel>();
        static List<ToDoListItemViewModel> expectedToDoList = new List<ToDoListItemViewModel>();


        [AssemblyInitialize]
        public static void InitializeTest(TestContext testContext)
        {
            Process.Start(@"..\..\..\..\ToDoListAPI\bin\Debug\netcoreapp3.1\ToDoListAPI.exe");
            FillDatabase();
        }

        [AssemblyCleanup]
        public static void CleanupAssembly()
        {
            Process[] _proceses = null;
            _proceses = Process.GetProcessesByName("ToDoListAPI");
            foreach (Process proces in _proceses)
            {
                proces.Kill();
            }
        }

        [TestMethod]
        public void ToDoListTest1()
        {
            Assert.IsTrue(TestList(), "Retreive ToDo List Failed");
        }
        [TestMethod]
        public void ToDoListTest2()
        {
            Assert.IsTrue(TestLists(), "Retrive All ToDo Lists Failed");
        }

        private static void FillDatabase()
        {
            ToDoListViewModel toDoList = new ToDoListViewModel
            {
                id = 1,
                name = "Test List 1",
                associatedWith = "Associated 1"
            };

            ToDoListItemViewModel toDoListItem = new ToDoListItemViewModel
            {
                id = 1,
                listId = 1,
                description = "Test Item 1",
                completed = false
            };
                     

            expectedToDoLists.Add(toDoList);
            expectedToDoList.Add(toDoListItem);

            AddList(toDoList);
            AddItem(toDoListItem);

            toDoList = new ToDoListViewModel
            {
                id = 2,
                name = "Test List 2",
                associatedWith = "Associated 2"
            };

            toDoListItem = new ToDoListItemViewModel
            {
                id = 2,
                listId = 2,
                description = "Test Item 2",
                completed = false
            };

            expectedToDoLists.Add(toDoList);

            AddList(toDoList);
            AddItem(toDoListItem);

            toDoListItem = new ToDoListItemViewModel
            {
                id = 3,
                listId = 1,
                description = "Test Item 3",
                completed = false
            };

            expectedToDoList.Add(toDoListItem);

            AddItem(toDoListItem);
        }

        private static bool TestList()
        {
            List<ToDoListItemViewModel> toDoList = GetList(1);

            bool test = toDoList.SequenceEqual(expectedToDoList, new ToDoListItemCompare());

            return test;
        }

        private static bool TestLists()
        {
            List<ToDoListViewModel> toDoLists = GetAllLists();

            bool test = toDoLists.SequenceEqual(expectedToDoLists, new ToDoListCompare());

            return test;
        }

        private static void AddList(ToDoListViewModel toDoList)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListViewModel>(toDoList), Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("http://localhost:5000/api/ToDoList", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }
        }

        private static void AddItem(ToDoListItemViewModel toDoListItem)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListItemViewModel>(toDoListItem), Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("http://localhost:5000/api/ToDoListItem", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }
        }

        private static List<ToDoListItemViewModel> GetList(int id)
        {
            List<ToDoListItemViewModel> toDoListItems = new List<ToDoListItemViewModel>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:5000/api/ToDoListItem/List/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoListItems = JsonSerializer.Deserialize<List<ToDoListItemViewModel>>(data.Result);
            }

            return toDoListItems;
            }

        private static List<ToDoListViewModel> GetAllLists()
        {
            List<ToDoListViewModel> toDoLists = new List<ToDoListViewModel>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:5000/api/ToDoList/All");
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoLists = JsonSerializer.Deserialize<List<ToDoListViewModel>>(data.Result);
            }

            return toDoLists;
        }
    }
}
