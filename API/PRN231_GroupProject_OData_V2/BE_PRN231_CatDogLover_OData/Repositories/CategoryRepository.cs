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
    public class CategoryRepository : ICategoryRepository
    {
        public Category AddCategory(Category Category) => CategoryDAO.Instance.AddCategory(Category);

        public IEnumerable<Category> GetAll() => CategoryDAO.Instance.GetAll();

        public Category GetByID(int id) => CategoryDAO.Instance.GetByID(id);

        public Category RemoveCategory(int id) => CategoryDAO.Instance.RemoveCategory(id);

        public Category UpdateCategory(Category Category) => CategoryDAO.Instance.UpdateCategory(Category);
    }
}
