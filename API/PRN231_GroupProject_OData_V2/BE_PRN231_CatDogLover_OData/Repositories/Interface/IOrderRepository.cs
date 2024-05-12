using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IOrderRepository
    {
        Order GetByID(int id);
        Order AddOrder(Order Order);
        Order RemoveOrder(int id);
        Order UpdateOrder(Order Order);
        IEnumerable<Order> GetAll();
    }
}
