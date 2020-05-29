using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksMVC.Domain;
using BooksMVC.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;


namespace BooksMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksDataContext _dataContext;

        public BooksController(BooksDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromInventory(int bookId)
        {
            var book = await _dataContext.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            book.InInventory = false;
            await _dataContext.SaveChangesAsync();
            TempData["flash"] = $"Removed {book.Title} from inventory.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            // TODO:If it isn't there, send a 404
            var bookToEdit = await _dataContext.Books
                .Select(b => new BookEdit
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    NumberOfPages = b.NumberOfPages,
                    PublicationDate = new DateTime(1969,4,20)
                })
                .SingleOrDefaultAsync(b => b.Id == id);

            return View(bookToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BookEdit editedBook)
        {
            // Todo: Validate it, update it, redirect
            if(!ModelState.IsValid)
            {
                return View("Edit", editedBook);
            } else
            {
                var storedBook = await _dataContext.Books
                    .SingleOrDefaultAsync(b => b.Id == editedBook.Id);
                storedBook.Title = editedBook.Title;
                storedBook.Author = editedBook.Author;
                storedBook.NumberOfPages = editedBook.NumberOfPages;
                await _dataContext.SaveChangesAsync();
                TempData["flash"] = "Updated your book";
                return RedirectToAction("Index");

            }
        }

        public IActionResult New()
        {

            return View(new BookCreate());
        }

        [HttpPost("/books")]
        public async Task<IActionResult> Create(BookCreate bookToAdd)
        {
            if(!ModelState.IsValid)
            {
                return View("New", bookToAdd);
            } else
            {
                // add it to the database and stuff. TODO
                var book = new Book
                {
                    Title = bookToAdd.Title,
                    Author = bookToAdd.Author,
                    InInventory = true,
                    NumberOfPages = bookToAdd.NumberOfPages
                };
                _dataContext.Books.Add(book);
                await _dataContext.SaveChangesAsync();
                TempData["flash"] = $"Book {book.Title} add as {book.Id}";
                return RedirectToAction("New"); // PRG
            }
        }

        [HttpGet("/books/{bookId:int}")]
        public async Task<IActionResult> Details(int bookId)
        {
            var response = await _dataContext.Books.Where(b => b.Id == bookId)
                .Select(b=> new GetSingleBookResponseModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    InInventory = b.InInventory,
                    NumberOfPages = b.NumberOfPages
                })
                .SingleOrDefaultAsync();
            if(response == null)
            {
                return NotFound("No Book with that Id");
            } else
            {
                return View(response);
            }
        }

        // GET /books
        // GET /books/index
        // GET /books?showall=true
        public async Task<IActionResult> Index([FromQuery] bool showall = false)
        {
            //// NO Model. just serializing the domain objects.
            //var response = await _dataContext.Books.Where(b => b.InInventory).ToListAsync();
            //return View(response);
            ViewData["sale"] = "All Books are 20% Off Until Friday";
            var response = new GetBooksResponseModel
            {
                Books = await _dataContext.Books.Where(b => b.InInventory).Select(b => new BooksResponseItemModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author
                }).ToListAsync(),
                NumberOfBooksInInventory = await _dataContext.Books.CountAsync(b => b.InInventory),
                NumberOfBooksNotInInventory = await _dataContext.Books.CountAsync(b => b.InInventory == false)
            };
            if(showall)
            {
                response.BooksNotInInventory = await _dataContext.Books.Where(b => b.InInventory == false)
                    .Select(b => new BooksResponseItemModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author
                    }).ToListAsync();
            }
            return View(response);
        }
    }
}
