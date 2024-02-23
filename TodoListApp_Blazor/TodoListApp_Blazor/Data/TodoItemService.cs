using Newtonsoft.Json;

namespace TodoListApp_Blazor.Data
{
    public class TodoItemService
    {
        private static List<TodoItem> _data = new List<TodoItem>()
        {
           new TodoItem {Title = "Learn C# ASP.NET Core MVC" },
           new TodoItem {Title = "Learn C# ASP.NET Core Razor page" },
           new TodoItem {Title = "Learn C# ASP.NET Core Blazor" },
           new TodoItem {Title = "Learn C# ASP.NET Core Web API" },
           new TodoItem {Title = "Build a Project C# ASP.CORE" },
        };
        private readonly string _file = "Data\\todo.json";
        public List<TodoItem> GetData()
        {
            if (File.Exists(_file)) 
            {
                using var file = File.OpenText(_file);
                var serializer = new JsonSerializer();
                _data = serializer.Deserialize(file, typeof(List<TodoItem>)) as List<TodoItem>;
            }
            return _data;
        }
        public void SaveChanges()
        {
            using var file = File.CreateText(_file);
            var serializer = new JsonSerializer();
            serializer.Serialize(file, _data);
        }
    }
}
