using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace Models.DAO
{
    public class RoleDAO
    {
        FastNewsDbContext db = null;
        public RoleDAO()
        {
            db = new FastNewsDbContext();
        }

        public List<Role> ListAll()
        {
            return db.Roles.Where(x => x.IsDisable == false).OrderByDescending(x=>x.RoleID).ToList();
        }

        public Role GetById(string roleName)
        {
            return db.Roles.SingleOrDefault(x => x.RoleName == roleName);
        }

        public Role ViewDetail(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Role> model = db.Roles;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.RoleName.Contains(searchString));
            }
            return model.OrderBy(x => x.RoleID).ToPagedList(page, pageSize);
        }

        public long Insert(Role entity)
        {
            // kiem tra trung ten
            var roleName = this.db.Roles.Where(x => x.RoleName == entity.RoleName).Count();
            if (roleName != 0)
            {
                return -1;
            }
            entity.IsDisable = false;

            db.Roles.Add(entity);
            db.SaveChanges();

            return entity.RoleID;
        }

        public bool Delete(int id)
        {
            try
            {
                var role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Update(Role entity)
        {
            try
            {
                // kiem tra xem tên quyen duoc cap nhat da co tren he thong chua
                // neu co thi tra ve -1
                var roleName = this.db.Roles.Where(x => x.RoleName == entity.RoleName && x.RoleID != entity.RoleID).Count();
                if (roleName != 0)
                {
                    return -1;
                }
                var role = db.Roles.Find(entity.RoleID);
                role.RoleName = entity.RoleName;
                role.IsDisable = entity.IsDisable;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                //logging
                return 0;
            }
        }

    }
}
