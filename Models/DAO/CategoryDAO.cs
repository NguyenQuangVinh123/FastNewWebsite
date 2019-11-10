using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        FastNewsDbContext db = null;
        public CategoryDAO()
        {
            db = new FastNewsDbContext();
        }

        public Category GetById(string categoryName)
        {
            return db.Categories.SingleOrDefault(x => x.CategoryName == categoryName);
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.ShowOnMenu == true).ToList();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.CategoryName.Contains(searchString));
            }
            return model.OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CategoryID;
        }

        public int Delete(int id)
        {
            try
            {
                var cate = db.Categories.Find(id);
                var checkExistPost = this.db.Posts.Count(x => x.CategoryID == id) > 0;
                if (checkExistPost)
                {
                    return -1;
                }
                db.Categories.Remove(cate);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Category entity)
        {
            try
            {
                var cate = db.Categories.Find(entity.CategoryID);
                cate.CategoryName = entity.CategoryName;
                cate.MetaTitle = entity.MetaTitle;
                cate.DisplayOrder = entity.DisplayOrder;
                cate.ShowOnMenu = entity.ShowOnMenu;
                cate.ShowOnHome = entity.ShowOnHome;
                cate.Target = entity.Target;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }

        //Add function in Client web
        public List<Category> ShowMenuCategory()
        {
            return this.db.Categories.Where(x => x.ShowOnMenu == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
