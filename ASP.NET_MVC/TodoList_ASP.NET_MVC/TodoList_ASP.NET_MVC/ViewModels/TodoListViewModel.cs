using Dapper;
using TodoList_ASP.NET_MVC.Helpers;
using TodoList_ASP.NET_MVC.Models;

namespace TodoList_ASP.NET_MVC.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel() 
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new TodoListItem();
                this.TodoItems = db.Query<TodoListItem>("SELECT * FROM TodoListItems ORDER BY AddDate DESC").ToList();
            }
        }
        public List<TodoListItem> TodoItems { get; set; }
        public TodoListItem EditableItem { get; set; }
    }
}
