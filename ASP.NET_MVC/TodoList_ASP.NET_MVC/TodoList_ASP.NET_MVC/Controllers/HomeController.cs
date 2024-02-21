using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList_ASP.NET_MVC.Helpers;
using TodoList_ASP.NET_MVC.Models;
using TodoList_ASP.NET_MVC.ViewModels;

namespace TodoList_ASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();  
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);   
                if (item != null)
                {
                    db.Delete(item);
                }
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<TodoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        TodoListItem dbItem = db.Get<TodoListItem>(viewModel.EditableItem.Id);
                        var result = TryUpdateModelAsync<TodoListItem>(dbItem, "EditableItem");
                        db.Update<TodoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", new TodoListViewModel());
            }
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null) 
                { 
                    item.IsDone = !item.IsDone;
                    db.Update<TodoListItem>(item);
                }
            }
            return RedirectToAction("Index");   
        }
    }
}