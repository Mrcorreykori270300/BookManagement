using BookManagement.Data;
using BookManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class BooksController : Controller
    {
        public readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Book> bookObject = _db.Books.ToList();
            return View(bookObject);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Book bookObj)
        {
            if (bookObj.Title != null)
            {
                // Check if the book title already exists in the database
                Book? book = _db.Books.FirstOrDefault(e => e.Title == bookObj.Title);
                if (book == null) 
                {
                    if (ModelState.IsValid)
                    {
                        _db.Books.Add(bookObj);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "Books");
                    }
                    return View();
                }
                else 
                {
                    ModelState.AddModelError("Title", "Book with this title already exists.");
                    return View();
                }
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book book = _db.Books.FirstOrDefault(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book bookObj)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(bookObj);
                _db.SaveChanges();

                return RedirectToAction("Index", "Books");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book? book = _db.Books.FirstOrDefault(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book objBook)
        {
            Book? category = _db.Books.FirstOrDefault(u => u.Id == objBook.Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Books.Remove(category);
            //_db.Database.ExecuteSqlRaw("EXEC DeleteCategory @CategoryId", new SqlParameter("@CategoryId", objCategory.Id));
            _db.SaveChanges();

            return RedirectToAction("Index", "Books");
        }
    }
}
