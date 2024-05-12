using BusinessObjects;
using DataAccess;
using DTOs.Pagination;
using DTOs.Post;
using Repositories.Interface;
using Repositories.Utility;
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

        public async Task<Post> GetPostByIdAsync(int id)
        {
            try
            {
                var post = await PostDAO.Instance.GetByIDAsync(id);
                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedList<Post> Search(PostSearchRequest searchRequest)
        {
            var query = PostDAO.Instance.GetAll().AsQueryable();
            // Apply search
            query = query.GetWithSearch(searchRequest).AsQueryable();
            // Apply sort
            //response.GetWithSort();
            // Apply pagination
            var pagingData = query.GetWithPaging(searchRequest);

            return pagingData;
        }
    }
}
