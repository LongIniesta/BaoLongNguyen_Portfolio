using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Create(Product Product);
        void DeleteById(int id);
        void Update(Product Product);
    }
}
