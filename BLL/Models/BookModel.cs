using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class BookModel
    {
        public Book Record { get; set; }

        [DisplayName("Book Name")]
        public string Name => Record.Name;

        [DisplayName("Number of Pages")]
        public string NumberOfPages => Record.NumberOfPages.HasValue ? Record.NumberOfPages.ToString() : "N/A";

        [DisplayName("Publish Date")]
        public string PublishDate => Record.PublishDate.HasValue ? Record.PublishDate.Value.ToString("MM/dd/yyyy") : "N/A";

        [DisplayName("Price")]
        public string Price => Record.Price.HasValue ? Record.Price.Value.ToString("C2") : "N/A";

        [DisplayName("Top Seller")]
        public string IsTopSeller => Record.IsTopSeller ? "Yes" : "No";

        [DisplayName("Author")]
        public string Author => Record.Author?.Name + " " + Record.Author?.Surname;

        public string Genres => string.Join("<br>", Record.BookGenres?.Select(bg => bg.Genre?.Name) ?? Enumerable.Empty<string>());
       
        [DisplayName("Genres")]
        public List<int> GenreIds
        {
            get => Record.BookGenres?.Select(bg => bg.GenreId).ToList();
            set => Record.BookGenres = value.Select(v => new BookGenre() { GenreId = v }).ToList();
        }
    }
}
