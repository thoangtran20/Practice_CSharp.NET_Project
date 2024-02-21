using System.ComponentModel.DataAnnotations;

namespace TodoList_ASP.NET_MVC.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public DateTime AddDate {  get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least two characters!")]
        [MaxLength(200, ErrorMessage = "Title must contain at maximum of 200 characters!")]      
        public string Title { get; set; }
        
        public bool IsDone { get; set; }
    
    }
}
