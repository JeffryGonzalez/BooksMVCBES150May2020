using BooksMVC.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMVC.ViewComponents
{
    public class SalesViewComponent : ViewComponent
    {
        private readonly BooksDataContext _dataContext;

        public SalesViewComponent(BooksDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(decimal percentOff)
        {
            var data = await _dataContext.Books
                .Take(2)
                .Select(b => new BookSalesModel
                {
                    Id = b.Id,
                    Slug = $"{b.Title} by {b.Author}",
                    PercentOff = percentOff
                }).ToListAsync();

            return View(data);
        }
    }

    public class BookSalesModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public decimal PercentOff { get; set; }
    }
}
