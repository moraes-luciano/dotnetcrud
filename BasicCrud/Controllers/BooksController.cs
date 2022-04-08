using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicCrud.Models;
using BasicCrud.Data;

namespace BasicCrud.Controllers
{
    public class BooksController : Controller
    {
        public AppDbContext _db { get; set; }

        public BooksController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> booksFound = _db.Books.OrderBy(obj=>obj.Id);
            return View(booksFound);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            if(id==null || id== 0)
            {
                return NotFound();
            }

            Book bookFound = _db.Books.Find(id);
            if (bookFound == null)
            {
                return NotFound();
            }
            return View(bookFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book obj)
        {

            if (ModelState.IsValid)
            {
                _db.Books.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
           
        }

        public IActionResult Delete(int? id)
        {
            if(id ==null || id == 0)
            {
                return NotFound();
            }
            
            Book bookFound = _db.Books.Find(id);
            if(bookFound == null)
            {
                return NotFound();
            }

            return View(bookFound);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            Book bookFound = _db.Books.Find(id);
            _db.Books.Remove(bookFound);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
