using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class GenreModel
    {
        public Genre Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Book Count")]
        public string BookCount => Record.BookGenres?.Count.ToString() ?? "0";
    }
}
