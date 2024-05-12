using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        OrderDetail GetByID(int id);
        OrderDetail AddOrderDetail(OrderDetail OrderDetail);
        OrderDetail RemoveOrderDetail(int id);
        OrderDetail UpdateOrderDetail(OrderDetail OrderDetail);
        IEnumerable<OrderDetail> GetAll();
    }
}
