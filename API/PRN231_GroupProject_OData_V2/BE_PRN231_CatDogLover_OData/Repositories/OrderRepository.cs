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
    public class OrderRepository : IOrderRepository
    {
        public Order AddOrder(Order Order) => OrderDAO.Instance.AddOrder(Order);

        public IEnumerable<Order> GetAll() => OrderDAO.Instance.GetAll();   

        public Order GetByID(int id) => OrderDAO.Instance.GetByID(id);

        public Order RemoveOrder(int id) => OrderDAO.Instance.RemoveOrder(id);

        public Order UpdateOrder(Order Order) => OrderDAO.Instance.UpdateOrder(Order);
    }
}
