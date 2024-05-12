using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderComboDetailRepository : IOrderComboDetailRepository
    {
        public void Create(OrderComboDetail OrderComboDetail) => OrderComboDetailDAO.Instance.Add(OrderComboDetail);

        public void DeleteById(int id) => OrderComboDetailDAO.Instance.DeleteById(id);

        public List<OrderComboDetail> GetAll() => OrderComboDetailDAO.Instance.GetAll().ToList();

        public OrderComboDetail GetById(int id) => OrderComboDetailDAO.Instance.GetById(id);

        public void Update(OrderComboDetail OrderComboDetail) => OrderComboDetailDAO.Instance.UpdateOrderComboDetail(OrderComboDetail);
    }
}
