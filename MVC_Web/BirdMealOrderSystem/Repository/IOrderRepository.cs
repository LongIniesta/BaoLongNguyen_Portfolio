using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(int id);
        void Create(Order Order);
        void DeleteById(int id);
        void Update(Order Order);
    }
}
