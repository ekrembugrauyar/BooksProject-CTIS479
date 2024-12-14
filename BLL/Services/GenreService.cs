using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreService : ServiceBase, IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(g => (g.Name.ToUpper() == record.Name.ToUpper().Trim())))
            {
                return Error("Genre already exists!");
            }

            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.BookGenres).SingleOrDefault(a => a.Id == id);
            if (entity is null)
                return Error("Genre can't be found!");
            if (entity.BookGenres.Any())
                return Error("Genre has books!");
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.Include(g => g.BookGenres).OrderBy(g => g.Name).Select(g => new GenreModel() { Record = g });
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(g => g.Id != record.Id && g.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Same genre already exists!");

            var entity = _db.Genres.SingleOrDefault(a => a.Id == record.Id);

            if (entity is null)
                return Error("Genre can't be found!");

            entity.Name = record.Name?.Trim();

            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }
    }
}
