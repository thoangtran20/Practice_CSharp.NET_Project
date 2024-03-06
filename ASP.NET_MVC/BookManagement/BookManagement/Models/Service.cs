using System.Xml.Serialization;

namespace BookManagement.Models
{
    public class Service
    {
        public readonly string _dataFile = @"Data\data.xml";
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(HashSet<Book>));
        public HashSet<Book> Books { get; set; }
        public Service() 
        { 
            if (!File.Exists(_dataFile))
            {
                Books = new HashSet<Book>()
                {
                    new Book{Id = 1, Name = "Ghi Chép Pháp Y - Những Thi Thể Không Hoàn Chỉnh", 
                        Author = "Lưu Bát Bách", Publisher = "AZ Việt Nam", Year = 2023},
                    new Book{Id = 2, Name = "Eureka! Khoảnh Khắc Sáng Tạo Xuất Thần",
                        Author = "Yew Kam Keong", Publisher = "Bách Việt", Year = 2020},
                    new Book{Id = 3, Name = "Càng Bình Tĩnh Càng Hạnh Phúc",
                        Author = "Vãn Tình", Publisher = "AZ Việt Nam", Year = 2022},
                    new Book{Id = 4, Name = "Càng Độc Lập Càng Cao Quý",
                        Author = "Vãn Tình", Publisher = "1980 Books", Year = 2022},
                    new Book{Id = 5, Name = "Không Nỗ Lực Đừng Tham Vọng",
                        Author = "Lý Thượng Long", Publisher = "Minh Long", Year = 2023}
                };
            }
            else
            {
                using var stream = File.OpenRead(_dataFile);
                Books = _serializer.Deserialize(stream) as HashSet<Book>;
            }
        }

        public Book[] Get() => Books.ToArray();
        public Book Get(int id) => Books.FirstOrDefault(b => b.Id == id);

        public bool Add(Book book) => Books.Add(book);

        //public Book Create(Book book)
        //{
        //    var max = Books.Max(b => b.Id);
        //    var newBook = new Book()
        //    {
        //        Id = max + 1,
        //        Name = book.Name,
        //        Author = book.Author,
        //        Publisher = book.Publisher,
        //        Year = DateTime.Now.Year,
        //        Description = book.Description,
        //        DataFile = book.DataFile
        //    };
        //    return newBook;
        //}

        public Book Create()
        {
            var max = Books.Max(b => b.Id);
            var newBook = new Book()
            {
                Id = max + 1,
                Year = DateTime.Now.Year,
            };
            return newBook;
        }

        //public bool Update(Book book)
        //{
        //    var b = Get(book.Id);
        //    return b != null && Books.Remove(b) && Books.Add(book);
        //}

        public bool Update(Book book)
        {
            var existingBook = Get(book.Id);

            if (existingBook != null)
            {
                // Update the properties of the existing book
                existingBook.Name = book.Name;
                existingBook.Author = book.Author;
                existingBook.Publisher = book.Publisher;
                existingBook.Year = book.Year;
                existingBook.Description = book.Description;
                existingBook.DataFile = book.DataFile;

                return true;
            }

            return false; // Book not found
        }
        public bool Delete(int id)
        {
            var b = Get(id);
            return b != null && Books.Remove(b);
        }
        public void SaveChanges()
        {
            using var stream = File.Create(_dataFile);
            _serializer.Serialize(stream, Books);
        }
    }   
}
