using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AuthorService : ServiceBase, IService<Author,AuthorModel>
    {
        public AuthorService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Author record)
        {
            if (_db.Authors.Any(a => (a.Name.ToUpper() == record.Name.ToUpper().Trim()) && a.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Author with the same name and surname exists!");
            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();

            _db.Authors.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Author created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Authors.Include(a => a.Books).SingleOrDefault(a => a.Id == id);
            if (entity is null)
                return Error("Author can't be found!");
            if (entity.Books.Any())
                return Error("Author has relational books!");
            _db.Authors.Remove(entity);
            _db.SaveChanges();
            return Success("Author deleted successfully.");
        }

        public IQueryable<AuthorModel> Query()
        {
            return _db.Authors.Include(a => a.Books).OrderBy(a => a.Name).Select(a => new AuthorModel() { Record = a });
        }

        public ServiceBase Update(Author record)
        {
            if (_db.Authors.Any(a => a.Id != record.Id && a.Name.ToUpper() == record.Name.ToUpper().Trim() && a.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Author with the same name and surname exists!");

            var entity = _db.Authors.SingleOrDefault(a => a.Id == record.Id);

            if (entity is null)
                return Error("Author can't be found!");

            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();

            _db.Authors.Update(entity);
            _db.SaveChanges();
            return Success("Author updated successfully.");
        }
    }
}
