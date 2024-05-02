using System.ComponentModel.DataAnnotations;

namespace BlazorDapperSPA.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
    }
}
