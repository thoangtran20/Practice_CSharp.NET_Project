using System.Drawing;
using System.Linq.Dynamic.Core;
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

        public bool Update(Book book, IFormFile file)
        { 
            var existingBook = Get(book.Id);

            if (existingBook != null)
            {
                // Update the properties of the existing book
                existingBook.Name = book.Name;
                existingBook.Author = book.Author;
                existingBook.Publisher = book.Publisher;
                existingBook.Year = book.Year;
                existingBook.DataFile = book.DataFile;
                existingBook.Description = book.Description;
                // Check if a new file is provided
                if (file != null)
                {
                    // Remove the old file (if it exists)
                    if (!string.IsNullOrEmpty(existingBook.DataFile))
                    {
                        // Remove the old file
                        RemoveDataFile(existingBook.DataFile);
                    }
                    // Upload the new file
                    book.DataFile = Upload(existingBook, file);
                    
                }

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

        public string GetDataPath(string file) => $"Data\\{file}";
        public string Upload(Book book, IFormFile file)
        {
            if (file != null)
            {
                var path = GetDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                book.DataFile = file.FileName;

                SaveChanges();

                return file.FileName;
            }
            return null;
        }
        private void RemoveDataFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var path = GetDataPath(fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        public (Stream, string) Download(Book b)
        {
            var memory = new MemoryStream();
            using var stream = new FileStream(GetDataPath(b.DataFile), FileMode.Open);
            stream.CopyTo(memory);
            memory.Position = 0;
            var type = Path.GetExtension(b.DataFile) switch
            {
                "pdf" => "application/pdf",
                "docx" => "application/vnd.ms-word",
                "doc" => "application/vnd.ms-word",
                "txt" => "text/plain",
                _ => "application/pdf"
            };
            return (memory, type);
        }

        //public Book[] Get(string search)
        //{
        //    var s = search?.ToLower();

        //    if (string.IsNullOrEmpty(s))
        //    {
        //        return Books.ToArray();
        //    }

        //    return Books.Where(b =>
        //        b.Name.ToLower().Contains(s) ||
        //        b.Author.ToLower().Contains(s) ||
        //        b.Publisher.ToLower().Contains(s) ||
        //        b.Description.Contains(s) ||
        //        b.Year.ToString() == s
        //    ).ToArray();
        //}

        public (Book[] books, int pages, int page) Get(int page, string orderBy="Name", bool dsc = false, string search = "")
        {
            int size = 5;
            var s = search?.ToLower();
            var filteredBooks = string.IsNullOrEmpty(s)
               ? Books
               : Books.Where(b =>
                   b.Name.ToLower().Contains(s) ||
                   b.Author.ToLower().Contains(s) ||
                   b.Publisher.ToLower().Contains(s) ||
                   b.Description.Contains(s) ||
                   b.Year.ToString() == s
               );

            int totalItems = filteredBooks.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / size);

            var paginatedBooks = filteredBooks
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable()
                .OrderBy($"{orderBy} {(dsc ? "descending" : "")}")
                .ToArray();

            return (paginatedBooks, totalPages, page);
        }


        public (Book[] books, int pages, int page) Paging(int page, string orderBy = "Name", bool dsc = false) {
            int size = 5;
            int pages = (int)Math.Ceiling((double)Books.Count / size);
            var books = Books.Skip((page - 1) * size).Take(size).AsQueryable().OrderBy($"{orderBy} {(dsc ? "descending" : "")}").ToArray();
            return (books, pages, page);
        }
    }   
}
