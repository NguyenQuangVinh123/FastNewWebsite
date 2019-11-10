
using Models.EF;

namespace Models.DAO
{
    public class CommentDAO
    {
        FastNewsDbContext db = null;
        public CommentDAO()
        {
            db = new FastNewsDbContext();
        }
        public long Insert(Comment entity)
        {
            db.Comments.Add(entity);
            db.SaveChanges();
            return entity.CommentID;
        }
    }
}
