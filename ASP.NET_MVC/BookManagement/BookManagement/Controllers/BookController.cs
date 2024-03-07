using BookManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly Service _service;
        public BookController(Service service)
        {
            _service = service;
        }

        public IActionResult Index(int page = 1, string orderBy = "Name", bool dsc = false, string term = "")
        {
            var model = _service.Get(page, orderBy, dsc, term);

            // If there are no items on the current page, redirect to the first page
            if (model.pages > 0 && page > model.pages)
            {
                return RedirectToAction("Search", new { page = 1, orderBy, dsc, term });
            }

            // Paging
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            // Sort
            ViewData["Name"] = false;
            ViewData["Author"] = false;
            ViewData["Publisher"] = false;
            ViewData["Year"] = false;
            ViewData[orderBy] = !dsc;   

            return View(model.books);
            //return View(_service.Get());
        }

        public IActionResult Detail(int id)
        {
            var b = _service.Get(id);
            if (b == null) return NotFound();
            else return View(b);
        }

        public IActionResult Delete(int id)
        {
            var b = _service.Get(id);
            if (b == null) return NotFound();
            else return View(b);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _service.Delete(book.Id);
            _service.SaveChanges();
            return RedirectToAction("Index");   
        }

        public IActionResult Edit(int id)
        {
            var b = _service.Get(id);
            if (b == null) return NotFound();
            else return View(b);
        }

        [HttpPost]
        public IActionResult Edit(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _service.Update(book, file);
                _service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Create() => base.View(_service.Create());
        [HttpPost]
        public IActionResult Create(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    _service.Upload(book, file);
                }
                _service.Add(book);
                _service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Read(int id)
        {
            var b = _service.Get(id);
            if (b == null) return NotFound();
            if (!System.IO.File.Exists(_service.GetDataPath(b.DataFile))) return NotFound();
            var (stream, type) = _service.Download(b);
            return File(stream, type, b.DataFile);
        }

        //public IActionResult Search(string term)
        //{
        //    return View("Index", _service.Get(term));
        //}

        public IActionResult Search(int page = 1, string orderBy = "Name", bool dsc = false, string term = "")
        {
            var model = _service.Get(page, orderBy, dsc, term);

            // Paging
            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;

            // Sort
            ViewData["Name"] = false;
            ViewData["Author"] = false;
            ViewData["Publisher"] = false;
            ViewData["Year"] = false;
            ViewData[orderBy] = !dsc;

            return RedirectToAction("Index", new { page, orderBy, dsc, term });
        }
    }
}
