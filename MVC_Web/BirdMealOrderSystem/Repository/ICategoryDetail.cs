using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryDetail
    {
        List<CategoryDetail> GetAll();
        CategoryDetail GetById(int id);
        void Create(CategoryDetail categoryDetail);
        void DeleteById(int id);
        void Update(CategoryDetail categoryDetail);

    }
}
