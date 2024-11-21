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
            if (_db.Books.Any(b => b.Name.ToUpper() == record.Name.ToUpper().Trim() && b.PublishDate.Equals(record.PublishDate) && b.Author.Equals(record.Author)))
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
            if (entity is null)
                return Error("Book can't be found!");
            _db.Books.Remove(entity);
            _db.SaveChanges();
            return Success("Book deleted successfully.");
        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Author).OrderBy(b => b.Name).Select(b => new BookModel() { Record = b });
        }

        public ServiceBase Update(Book record)
        {
            if (_db.Books.Any(b => b.Id != record.Id && b.Name.ToUpper() == record.Name.ToUpper().Trim() && b.PublishDate.Equals(record.PublishDate) && b.Author.Equals(record.Author)))
            {
                return Error("Book with same name, publish day and writer exists!");
            }
            record.Name = record.Name?.Trim();
            _db.Books.Update(record);
            _db.SaveChanges();
            return Success("Book updated successfully.");
        }
    }
}
