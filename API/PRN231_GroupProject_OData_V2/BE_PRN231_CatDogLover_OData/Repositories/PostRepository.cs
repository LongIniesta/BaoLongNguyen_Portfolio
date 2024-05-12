using BusinessObjects;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PostRepository : IPostRepository
    {
        public Post AddPost(Post Post) => PostDAO.Instance.AddPost(Post);

        public IEnumerable<Post> GetAll() => PostDAO.Instance.GetAll();

        public Post GetByID(int id) => PostDAO.Instance.GetByID(id);

        public Post RemovePost(int id) => PostDAO.Instance.RemovePost(id);

        public Post UpdatePost(Post Post) => PostDAO.Instance.UpdatePost(Post);
    }
}
