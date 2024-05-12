using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderComboDetailRepository
    {
        List<OrderComboDetail> GetAll();
        OrderComboDetail GetById(int id);
        void Create(OrderComboDetail OrderComboDetail);
        void DeleteById(int id);
        void Update(OrderComboDetail OrderComboDetail);
    }
}
