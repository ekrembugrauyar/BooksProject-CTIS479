using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class BookService : ServiceBase, IService<Book, BookModel>
    {
        public BookService(Db db) : base(db)
        {

        }

        public ServiceBase Create(Book record)
        {
            if (_db.Books.Any(b => b.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                return Error("Book already exists!");
            }
            record.Name = record.Name?.Trim();
            _db.Books.Add(record);
            _db.SaveChanges();
            return Success("Book created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Books.Include(b => b.BookGenres).SingleOrDefault(b => b.Id == id);
            if (entity == null)
                return Error("Book can't be found!");
            _db.BookGenres.RemoveRange(entity.BookGenres);
            _db.Books.Remove(entity);
            _db.SaveChanges();
            return Success("Book deleted successfully.");
        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Author).Include(b => b.BookGenres).ThenInclude(bg => bg.Genre).OrderBy(b => b.Name).Select(b => new BookModel() { Record = b });
        }

        public ServiceBase Update(Book record)
        {
            if (_db.Books.Any(b => b.Id != record.Id && b.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                return Error("Book with the same name exists!");
            }

            var entity = _db.Books.Include(b => b.BookGenres).SingleOrDefault(b => b.Id == record.Id);
            if (entity == null)
            {
                return Error("Book not found!");
            }

            _db.BookGenres.RemoveRange(entity.BookGenres);
            entity.Name = record.Name?.Trim();
            entity.NumberOfPages = record.NumberOfPages;
            entity.PublishDate = record.PublishDate;
            entity.Price = record.Price;
            entity.IsTopSeller = record.IsTopSeller;
            entity.AuthorId = record.AuthorId;
            entity.BookGenres = record.BookGenres;

            _db.Books.Update(entity);
            _db.SaveChanges();
            return Success("Book updated successfully.");
        }
    }
}
