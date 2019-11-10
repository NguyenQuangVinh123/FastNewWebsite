using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Models.ViewModel;

namespace Models.DAO
{
    public class PostDAO
    {
        FastNewsDbContext db = null;
        public PostDAO()
        {
            db = new FastNewsDbContext();
        }

        public Post GetById(string postName)
        {
            return db.Posts.SingleOrDefault(x => x.Title == postName);
        }

        public Post ViewDetail(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString));
            }
            return model.OrderBy(x => x.Title).ToPagedList(page, pageSize);
        }

        public long Insert(Post entity)
        {
            db.Posts.Add(entity);
            db.SaveChanges();
            return entity.PostID;
        }

        public bool Delete(int id)
        {
            try
            {
                var post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Post entity)
        {
            try
            {
                var post = db.Posts.Find(entity.PostID);
                post.Title = entity.Title;
                post.MetaTitle = entity.MetaTitle;
                post.Decription = entity.Decription;
                post.Image = entity.Image;
                post.ContentDetail = entity.ContentDetail;
                post.CategoryID = entity.CategoryID;

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

        public List<Post> GetRecentPost()
        {
            var post = db.Posts.OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
            List<PostViewModel> result = new List<PostViewModel>();
            foreach (var item in post)
            {
                var categoryMetaTitle = this.db.Categories.Find(item.CategoryID).MetaTitle;

                // Gán đỡ giá trị MetaTitle của thằng Category vào nội dung của thằng Post
                item.ContentDetail = categoryMetaTitle;
            }

            return post;
        }

        public List<PostViewModel> GetTop3Post(string exceptHome)
        {
            List<PostViewModel> listResult = new List<PostViewModel>();
            List<int> listCategoryID = this.db.Categories.Where(x => x.ShowOnHome == true && x.MetaTitle != string.Empty && x.MetaTitle != exceptHome && x.MetaTitle != exceptHome.Replace("-", " ")).Select(x => x.CategoryID).ToList();


            foreach (var itemID in listCategoryID)
            {
                var category = this.db.Categories.Find(itemID);

                var post = (from p in this.db.Posts
                            join c in this.db.Categories
                                on p.CategoryID equals c.CategoryID
                            where c.CategoryID == itemID && c.ShowOnHome == true
                            select p).OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
                listResult.Add(new PostViewModel()
                {
                    CategoryName = category.CategoryName,
                    CategoryMetaTitle = category.MetaTitle,
                    ListPosts = post
                });
            }
            return listResult;
        }

        public List<Post> GetPostForSlide(string aside)
        {
            List<Post> post;
            if (aside == "left")
            {
               post = db.Posts.OrderByDescending(x => x.DatetimeCreate).Take(4).ToList();
            }
            else
            {
                post = db.Posts.OrderByDescending(x => x.DatetimeCreate).Skip(4).Take(4).ToList();
            }

            foreach (var item in post)
            {
                var categoryMetaTitle = this.db.Categories.Find(item.CategoryID).MetaTitle;

                // Gán đỡ giá trị MetaTitle của thằng Category vào nội dung của thằng Post
                item.ContentDetail = categoryMetaTitle;
            }

            return post;
        }

        public PostViewModel GetDetailCategoryList(int categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            PostViewModel viewResult = new PostViewModel();

            var category = this.db.Categories.Find(categoryID);
            totalRecord = this.db.Posts.Count(x => x.CategoryID == categoryID);
            var listPost = this.db.Posts.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.DatetimeCreate)
                .OrderByDescending(x => x.DatetimeCreate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            viewResult.CategoryName = category.CategoryName;
            viewResult.CategoryMetaTitle = category.MetaTitle;
            viewResult.ListPosts = listPost;

            return viewResult;
        }

        public List<Post> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            List<Post> viewResult = new List<Post>();
            totalRecord = this.db.Posts.Count(x => x.Title.Contains(keyword) || x.MetaTitle.Contains(keyword));
            var listPost = this.db.Posts.Where(x => x.Title.Contains(keyword) || x.MetaTitle.Contains(keyword)).OrderByDescending(x => x.DatetimeCreate)
                .OrderByDescending(x => x.DatetimeCreate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            foreach (var post in listPost)
            {
                post.ContentDetail = this.db.Categories.Find(post.CategoryID).CategoryName;
                viewResult.Add(post);
            }

            return viewResult;
        }

        public List<Post> GetSimmilarPost(int postID)
        {
            var cateId = this.db.Posts.Find(postID).CategoryID;
            var cateMeta = this.db.Categories.Find(cateId).MetaTitle;

            var listPost = (from p in this.db.Posts
                            join c in this.db.Categories
                                on p.CategoryID equals c.CategoryID
                            where p.PostID != postID && p.CategoryID == cateId
                            select p).OrderByDescending(x => x.DatetimeCreate).Take(3).ToList();
            foreach (var item in listPost)
            {
                item.ContentDetail = cateMeta;
            }
            return listPost;
        }

        public Post GetPrevOrNextPost(int postID, string howName)
        {

            List<Post> post = new List<Post>();
            if (howName == "PREV")
            {
                post = this.db.Posts.Where(x => x.PostID < postID).OrderByDescending(x => x.PostID).Take(1).ToList();
            }
            else
            {
                post = this.db.Posts.Where(x => x.PostID > postID).Take(1).ToList();
            }

            if (post.Count == 0)
            {
                return null;
            }

            var categorMetaTitle = this.db.Categories.Find(post[0].CategoryID).MetaTitle;

            // Gán đỡ giá trị MetaTitle của thằng Category vào nội dung của thằng Post
            post[0].ContentDetail = categorMetaTitle;
            return post[0];
        }

        public List<CommentViewModel> GetRecentComment(int postID)
        {
            var comment = (from c in this.db.Comments
                           join a in this.db.Accounts
                               on c.UserID equals a.AccountID
                           where c.PostID == postID
                           select new CommentViewModel() { Username = a.AccountName, DateTimeCreate = c.DateTimeCreate, ContentDetail = c.ContentDetail })
                .OrderByDescending(x => x.DateTimeCreate).Take(3).ToList();

            return comment;
        }
    }
}
