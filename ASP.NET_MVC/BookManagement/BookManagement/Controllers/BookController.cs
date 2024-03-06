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

        public IActionResult Index()
        {
            return View(_service.Get());
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
        public IActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                _service.Update(book);
                _service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Create() => base.View(_service.Create());
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                _service.Add(book);
                _service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}
