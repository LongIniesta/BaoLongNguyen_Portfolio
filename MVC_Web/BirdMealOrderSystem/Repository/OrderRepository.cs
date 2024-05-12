using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void Create(Order Order) => OrderDAO.Instance.Add(Order);

        public void DeleteById(int id) => OrderDAO.Instance.DeleteById(id);

        public List<Order> GetAll() => OrderDAO.Instance.GetAll().ToList();

        public Order GetById(int id) => OrderDAO.Instance.GetById(id);

        public void Update(Order Order) => OrderDAO.Instance.UpdateOrder(Order);
    }
}
