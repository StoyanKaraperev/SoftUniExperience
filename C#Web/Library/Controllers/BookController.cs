﻿namespace Library.Controllers
{
    using Library.Interfaces;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllBooksAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var model = await bookService.GetMyBookAsync(GetUserId());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id); 

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await bookService.AddBookToCollectionAsync(userId, book);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Mine));
            }

            var userId = GetUserId();

            await bookService.RemoveBookFromCollectionAsync(userId, book);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await bookService.GetNewAddBookModelAsync(); 

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            decimal rating; 

            if(!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number between 0 and 10");

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await bookService.AddBookAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddBookViewModel? book = await bookService.GetBookByIdForEditAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddBookViewModel model)
        {
            decimal rating;

            if (!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number beween 0 and 10");

                return View(model);
            }

            await bookService.EditBookAsync(model, id);

            return RedirectToAction(nameof(All));
        }
    }
}
