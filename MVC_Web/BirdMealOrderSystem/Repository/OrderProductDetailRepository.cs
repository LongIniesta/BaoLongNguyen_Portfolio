using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderProductDetailRepository : IOrderProductDetailRepository
    {
        public void Create(OrderProductDetail OrderProductDetail) => OrderProductDetailDAO.Instance.Add(OrderProductDetail);

        public void DeleteById(int id) => OrderProductDetailDAO.Instance.DeleteByid(id);

        public List<OrderProductDetail> GetAll() => OrderProductDetailDAO.Instance.GetAll().ToList();

        public OrderProductDetail GetById(int id) => OrderProductDetailDAO.Instance.GetById(id);

        public List<OrderProductDetail> GetByProductId(int productId) => OrderProductDetailDAO.Instance.GetByProductId(productId);

        public void Update(OrderProductDetail OrderProductDetail) => OrderProductDetailDAO.Instance.UpdateOrderProductDetail(OrderProductDetail);

    }
}
