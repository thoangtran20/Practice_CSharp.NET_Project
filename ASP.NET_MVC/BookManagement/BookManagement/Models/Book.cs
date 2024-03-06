using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, DisplayName("Tiêu đề")]
        public string Name { get; set; }
        [Required, DisplayName("Tác giả")]
        public string Author { get; set; }
        [Required, DisplayName("Nhà xuất bản")]
        public string Publisher { get; set; }
        [Required, Range(1990, int.MaxValue), DisplayName("Năm xuất bản")]
        public int Year { get; set; }
        [DisplayName("Tóm tắt")]
        public string Description { get; set; }
        [DisplayName("File")]
        public string DataFile { get; set; }
        
    }
}
