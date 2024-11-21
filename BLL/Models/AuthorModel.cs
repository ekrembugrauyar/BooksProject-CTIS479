using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class AuthorModel
    {
        public Author Record { get; set; }

        [DisplayName("Name")]
        public string Name => Record.Name;

        [DisplayName("Surname")]
        public string Surname => Record.Surname;
        
        [DisplayName("FullName")]
        public string FullName => Record.Name + " " + Record.Surname;

        [DisplayName("Books Count")]
        public string BooksCount => Record.Books?.Count.ToString() ?? "0";
    }
}
