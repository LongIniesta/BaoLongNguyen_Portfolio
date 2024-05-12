using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        Category GetByID(int id);
        Category AddCategory(Category Category);
        Category RemoveCategory(int id);
        Category UpdateCategory(Category Category);
        IEnumerable<Category> GetAll();
    }
}
