using ToDoListApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ToDoListApp.Controllers
{
    public class ToDoListController : Controller
    {
        public IActionResult AllToDoLists()
        {
            List<ToDoListViewModel> toDoList = new List<ToDoListViewModel>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoList/All");
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoList = JsonSerializer.Deserialize<List<ToDoListViewModel>>(data.Result);
            }

            return View(toDoList);
        }

        public IActionResult AllAssociatedToDoLists(int id)
        {
            List<ToDoListViewModel> toDoList = new List<ToDoListViewModel>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoList/AllAssociated/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoList = JsonSerializer.Deserialize<List<ToDoListViewModel>>(data.Result);
            }

            return View(toDoList);
        }

        public IActionResult AddToDoList(ToDoListViewModel toDoList)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListViewModel>(toDoList), Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("http://localhost:43416/api/ToDoList", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect("/ToDoList/AllToDoLists");
        }

        public IActionResult ShowToDoList(int id)
        {
            ToDoListViewModel toDoList = new ToDoListViewModel();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoList/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoList = JsonSerializer.Deserialize<ToDoListViewModel>(data.Result);
            }

            ViewBag.toDoList = toDoList;

            List<ToDoListItemViewModel> toDoListItems = new List<ToDoListItemViewModel>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoListItem/List/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoListItems = JsonSerializer.Deserialize<List<ToDoListItemViewModel>>(data.Result);
            }

            return View(toDoListItems);
        }

        public IActionResult AddToDoListItem(ToDoListItemViewModel toDoListItem)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListItemViewModel>(toDoListItem), Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("http://localhost:43416/api/ToDoListItem", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect("/ToDoList/ShowToDoList/" + toDoListItem.listId);
        }

        public IActionResult EditItem(int id)
        {
            ToDoListItemViewModel toDoListItem = new ToDoListItemViewModel();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoListItem/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoListItem = JsonSerializer.Deserialize<ToDoListItemViewModel>(data.Result);
            }

            return View(toDoListItem);
        }

        public IActionResult EditList(int id)
        {
            ToDoListViewModel toDoListItem = new ToDoListViewModel();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("http://localhost:43416/api/ToDoList/ToDoList/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();

                toDoListItem = JsonSerializer.Deserialize<ToDoListViewModel>(data.Result);
            }

            return View(toDoListItem);
        }

        public IActionResult MarkCompleted(int id)
        {
            HttpContent httpContent = null;

            using (var client = new HttpClient())
            {
                var responseTask = client.PatchAsync("http://localhost:43416/api/ToDoListItem/MarkCompleted/" + id, httpContent);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult DeleteToDoItem(int id)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.DeleteAsync("http://localhost:43416/api/ToDoListItem/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult DeleteToDoList(int id)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.DeleteAsync("http://localhost:43416/api/ToDoList/" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UpdateToDoListItem(ToDoListItemViewModel toDoListItem)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListItemViewModel>(toDoListItem), Encoding.UTF8, "application/json");

                var responseTask = client.PatchAsync("http://localhost:43416/api/ToDoListItem", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect("/ToDoList/ShowToDoList/" + toDoListItem.listId);
        }

        public IActionResult UpdateToDoList(ToDoListViewModel toDoList)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize<ToDoListViewModel>(toDoList), Encoding.UTF8, "application/json");

                var responseTask = client.PatchAsync("http://localhost:43416/api/ToDoList", content);
                responseTask.Wait();

                var result = responseTask.Result;

                var data = result.Content.ReadAsStringAsync();
                responseTask.Wait();
            }

            return Redirect("/ToDoList/AllToDoLists");
        }
    }
}
