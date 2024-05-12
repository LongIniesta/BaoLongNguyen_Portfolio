using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IProductRepository
    {
        Product GetByID(string id);
        Product AddProduct(Product Product);
        Product RemoveProduct(string id);
        Product UpdateProduct(Product Product);
        IEnumerable<Product> GetAll();
    }
}
