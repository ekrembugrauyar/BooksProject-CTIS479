using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RoleService : ServiceBase, IService<Role, RoleModel>
    {
        public RoleService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(r => (r.Name.ToUpper() == record.Name.ToUpper().Trim())))
                return Error("Same role already exists!");
            record.Name = record.Name?.Trim();

            _db.Roles.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Role created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(r => r.Users).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return Error("Role can't be found!");
            if (entity.Users.Any())
                return Error("This role has users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Include(r => r.Users).OrderBy(r => r.Name).Select(r => new RoleModel() { Record = r });
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(r => r.Id != record.Id && r.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This role already exists!");

            var entity = _db.Roles.SingleOrDefault(r => r.Id == record.Id);

            if (entity is null)
                return Error("Role can't be found!");

            entity.Name = record.Name?.Trim();

            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully.");
        }
    }
}
