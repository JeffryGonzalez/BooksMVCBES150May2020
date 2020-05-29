using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BooksMVC.Models.Books
{
    public class BookCreate : IValidatableObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DisplayName("Number of Pages")]
        [Range(1, int.MaxValue, ErrorMessage ="Number of Pages is Required")]
        public int NumberOfPages { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Author.ToLower().EndsWith("king") && Title.ToLower() == "it")
            {
                yield return new ValidationResult("Not that book. Please",
                    new string[] { nameof(Title), nameof(Author) });
            }
        }
    }
}
