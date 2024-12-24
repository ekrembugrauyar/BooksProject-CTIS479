using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize] //for session
    public class FavoritesController : MvcController
    {
        const string SESSIONKEY = "Favorites";

        private readonly HttpServiceBase _httpService;
        private readonly IService<Book, BookModel> _bookService;

        public FavoritesController(HttpServiceBase httpService, IService<Book, BookModel> bookService)
        {
            _httpService = httpService;
            _bookService = bookService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id")?.Value);

        private List<FavoritesModel> GetSession(int userId)
        {
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == userId).ToList();
        }

        public IActionResult Get()
        {
            return View("List", GetSession(GetUserId()));
        }

        public IActionResult Remove(int bookId)
        {
            var favorites = GetSession(GetUserId());
            var favoritesItem = favorites.FirstOrDefault(c => c.BookId == bookId);
            favorites.Remove(favoritesItem);
            _httpService.SetSession(SESSIONKEY, favorites);
            return RedirectToAction(nameof(Get));
        }

        // GET: /Favorites/Add?petId=17
        public IActionResult Add(int bookId)
        {
            int userId = GetUserId();
            var favorites = GetSession(userId);
            favorites = favorites ?? new List<FavoritesModel>();
            if (!favorites.Any(f => f.BookId == bookId))
            {
                var book = _bookService.Query().SingleOrDefault(b => b.Record.Id == bookId);
                var favoritesItem = new FavoritesModel()
                {
                    BookId = bookId,
                    UserId = userId,
                    BookName = book.Name
                };
                favorites.Add(favoritesItem);
                _httpService.SetSession(SESSIONKEY, favorites);
                TempData["Message"] = $"\"{book.Name}\" added to favorites.";
            }
            return RedirectToAction("Index", "Books");
        }
    }
}
