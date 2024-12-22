using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : ServiceBase, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public ServiceBase Create(User record)
        {
            if (_db.Users.Any(u => (u.UserName.ToUpper() == record.UserName.ToUpper().Trim())))
                return Error("This username already exists!");

            record.UserName = record.UserName?.Trim();
            record.Password = record.Password?.Trim();

            _db.Users.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("User created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return Error("User can't be found!");
            if (entity.IsActive)
                return Error("User is still active!");
            _db.Users.Remove(entity);
            _db.SaveChanges();
            return Success("User deleted successfully.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel()
            {
                Record = u
            });
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(u => u.Id != record.Id && u.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
                return Error("User with the username already exists!");

            var entity = _db.Users.SingleOrDefault(u => u.Id == record.Id);

            if (entity is null)
                return Error("User can't be found!");

            entity.UserName = record.UserName?.Trim();
            entity.Password = record.Password?.Trim();
            entity.IsActive = record.IsActive;
            entity.RoleId = record.RoleId;

            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully.");
        }
    }
}
