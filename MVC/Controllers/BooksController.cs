﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class BooksController : MvcController
    {
        // Service injections:
        private readonly IService<Book, BookModel> _bookService;
        private readonly IService<Author, AuthorModel> _authorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public BooksController(
            IService<Book, BookModel> bookService,
            IService<Author, AuthorModel> authorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _bookService = bookService;
            _authorService = authorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Books
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _bookService.Query().ToList();
            return View(list);
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _bookService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["AuthorId"] = new SelectList(_authorService.Query().ToList(), "Record.Id", "FullName");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _bookService.Create(book.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = book.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _bookService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Books/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookModel book)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _bookService.Update(book.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = book.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _bookService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _bookService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}