using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.DAO
{
    public class AccountDAO
    {
        FastNewsDbContext db = null;
        public AccountDAO()
        {
            db = new FastNewsDbContext();
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Accounts.SingleOrDefault(x => x.AccountName == userName);
            var admin = this.db.Roles.FirstOrDefault(x => x.RoleName == "ADMIN").RoleID;
            if (result == null)
            {
                return 0;
            }
            else
            if (result.IsLock)
            {
                return -1;
            }
            else
            {
                if (result.Password == passWord)
                {
                    if (result.RoleID != admin)
                    {
                        return -3;
                    }
                    return 1;
                }
                else
                    return -2;
            }
        }

        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.AccountName == userName);
        }

        public Account ViewDetail(int id)
        {
            return db.Accounts.Find(id);
        }

        public IEnumerable<Account> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.AccountName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.AccountName).ToPagedList(page, pageSize);
        }

        public long Insert(Account entity)
        {
            // kiem tra trung ten
            var accountName = this.db.Accounts.Where(x => x.AccountName == entity.AccountName).Count();
            if (accountName != 0)
            {
                return -1;
            }
            entity.IsLock = false;
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.AccountID;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Accounts.Find(id);
                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Update(Account entity,string checkPass)
        {
            try
            {
                // kiem tra xem tên tai khoan duoc cap nhat da co tren he thong chua
                // neu co thi tra ve -1
                var name = this.db.Accounts.Where(x => x.AccountName == entity.AccountName && x.AccountID != entity.AccountID).Count();
                if (name != 0)
                {
                    return -1;
                }

                var user = db.Accounts.Find(entity.AccountID);

                // kiem tra xem mat khau co bi doi hay khong
                // neu co thi lay mat khau moi
                if (!string.IsNullOrEmpty(entity.Password) && checkPass != user.Password)
                {
                    user.Password = entity.Password;
                }

                user.AccountName = entity.AccountName;
                user.Name = entity.Name;
                user.RoleID = entity.RoleID;
                user.IsLock = entity.IsLock;

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                //logging
                return 0;
            }
        }


        //Add function in Client web

        public int ChangePassword(Account entity)
        {
            try
            {
                var checkPass = this.db.Accounts.Count(x =>x.AccountName == entity.AccountName && x.Password == entity.Password) > 0;
                if (checkPass)
                {
                    return -1;
                }
                var account = this.db.Accounts.SingleOrDefault(x => x.AccountName == entity.AccountName);
                account.Password = entity.Password;
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
