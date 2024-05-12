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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail AddOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.Instance.AddOrderDetail(OrderDetail);

        public IEnumerable<OrderDetail> GetAll() => OrderDetailDAO.Instance.GetAll();

        public OrderDetail GetByID(int id) => OrderDetailDAO.Instance.GetByID(id);

        public OrderDetail RemoveOrderDetail(int id) => OrderDetailDAO.Instance.RemoveOrderDetail(id);

        public OrderDetail UpdateOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.Instance.UpdateOrderDetail(OrderDetail);
    }
}
