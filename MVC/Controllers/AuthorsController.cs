using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class AuthorsController : MvcController
    {
        // Service injections:
        private readonly IService<Author, AuthorModel> _authorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public AuthorsController(
            IService<Author, AuthorModel> authorService

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _authorService = authorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Authors
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _authorService.Query().ToList();
            return View(list);
        }

        // GET: Authors/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _authorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Authors/Create
        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Authors/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _authorService.Create(author.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = author.Record.Id });
                    //return RedirectToAction("Details", new { id = author.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(author);
        }

        // GET: Authors/Edit/5
        [Authorize(Roles = "User, Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _authorService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Authors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Edit(AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _authorService.Update(author.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = author.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(author);
        }

        // GET: Authors/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _authorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _authorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
