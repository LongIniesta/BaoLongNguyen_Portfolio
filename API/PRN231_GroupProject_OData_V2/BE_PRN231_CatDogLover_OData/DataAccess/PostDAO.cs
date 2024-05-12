using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static readonly object instanceLock = new object();
        private PostDAO() { }
        public static PostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            }
        }
        public Post GetByID(int id)
        {
            Post result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Posts.Include(p => p.Gifts).Include(p => p.Products).Include(p => p.Owner).Include(p => p.Services).Include(p => p.Reacts).SingleOrDefault(u => u.PostId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }



        public Post AddPost(Post Post)
        {
            Post result;
            try
            {
                var DBContext = new PRN231Context();
                Post.Owner = null;
                Post.Products = null;
                Post.Services = null;
                Post.Gifts = null;
                result = DBContext.Posts.Add(Post).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Post RemovePost(int id)
        {
            Post result;
            Post Post = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Posts.Remove(Post).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Post UpdatePost(Post Post)
        {
            Post result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Posts.Update(Post).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Post> GetAll()
        {
            List<Post> result = new List<Post>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Posts.Include(p => p.Gifts).Include(p =>p.Products).Include(p => p.Owner).Include(p => p.Services).Include(p => p.Reacts).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
