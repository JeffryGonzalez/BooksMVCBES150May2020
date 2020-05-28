using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMVC.Models.Books
{
    public class GetBooksResponseModel
    {
        public List<BooksResponseItemModel> Books { get; set; }
        public List<BooksResponseItemModel> BooksNotInInventory { get; set; }
        public int NumberOfBooksInInventory { get; set; }
        public int NumberOfBooksNotInInventory { get; set; }
    }

    public class BooksResponseItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
