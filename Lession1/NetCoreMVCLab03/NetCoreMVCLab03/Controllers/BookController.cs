using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLab03.Models;

namespace NetCoreMVCLab03.Controllers
{
    public class BookController : Controller
    {
        protected Book book = new Book();

        public IActionResult Index()
        {
            ViewBag.Authors = book.Authors;
            ViewBag.Genres = book.Genres;
            var books = book.GetBookList();
            return View(books);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = book.Authors;
            ViewBag.Genres = book.Genres;
            Book model = new Book();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                // Xử lý lưu dữ liệu ở đây
                return RedirectToAction("Index");
            }

            ViewBag.Authors = book.Authors;
            ViewBag.Genres = book.Genres;
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Authors = book.Authors;
            ViewBag.Genres = book.Genres;
            var bookItem = book.GetBookById(id);
            return View(bookItem);
        }

        [HttpPost]
        public IActionResult Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                // Xử lý cập nhật dữ liệu ở đây
                return RedirectToAction("Index");
            }

            ViewBag.Authors = book.Authors;
            ViewBag.Genres = book.Genres;
            return View(model);
        }

        public PartialViewResult PopularBook()
        {
            var books = book.GetBookList();
            if (books == null)
            {
                books = new List<Book>(); // Trả về danh sách rỗng nếu null
            }
            return PartialView("_PopularBook", books);
        }
    }
}