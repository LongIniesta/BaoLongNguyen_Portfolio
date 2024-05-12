using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Create(Product Product) => ProductDAO.Instance.Add(Product);

        public void DeleteById(int id) => ProductDAO.Instance.DeleteById(id);

        public List<Product> GetAll() => ProductDAO.Instance.GetAll().ToList();

        public Product GetById(int id) => ProductDAO.Instance.GetById(id);

        public void Update(Product Product) => ProductDAO.Instance.UpdateProduct(Product); 
    }
}
