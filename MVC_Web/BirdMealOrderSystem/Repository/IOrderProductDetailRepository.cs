using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderProductDetailRepository
    {
        List<OrderProductDetail> GetByProductId(int productId);
        List<OrderProductDetail> GetAll();
        OrderProductDetail GetById(int id);
        void Create(OrderProductDetail OrderProductDetail);
        void DeleteById(int id);
        void Update(OrderProductDetail OrderProductDetail);
    }
}
