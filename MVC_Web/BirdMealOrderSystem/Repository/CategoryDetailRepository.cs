using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryDetailRepository : ICategoryDetail
    {
        public void Create(CategoryDetail categoryDetail) => CategoryDetailDAO.Instance.AddCategoryDetail(categoryDetail);

        public void DeleteById(int id) => CategoryDetailDAO.Instance.DeleteCategoryDetailById(id);

        public List<CategoryDetail> GetAll()  => CategoryDetailDAO.Instance.GetAll().ToList();

        public CategoryDetail GetById(int id) => CategoryDetailDAO.Instance.getCategoryDetailById(id);

        public void Update(CategoryDetail categoryDetail) => CategoryDetailDAO.Instance.UpdateCategoryDetail(categoryDetail);   
    }
}
