using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void addCategory(Category cat) => CategoryDAO.Instance.AddCategory(cat);

        public bool checkExits(Category cat) => CategoryDAO.Instance.checkExits(cat);

        public void removeCategory(Category cat) => CategoryDAO.Instance.DeleteCategory(cat);
    }
}
