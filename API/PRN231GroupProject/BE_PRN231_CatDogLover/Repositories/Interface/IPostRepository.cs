using BusinessObjects;
using DTOs.Account;
using DTOs.Pagination;
using DTOs.Post;
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
        Task<Post> GetPostByIdAsync(int id);
        PagedList<Post> Search(PostSearchRequest searchRequest);
    }
}
