using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IPostRepository
    {
        Post GetByID(int id);
        Post AddPost(Post Post);
        Post RemovePost(int id);
        Post UpdatePost(Post Post);
        IEnumerable<Post> GetAll();
    }
}
