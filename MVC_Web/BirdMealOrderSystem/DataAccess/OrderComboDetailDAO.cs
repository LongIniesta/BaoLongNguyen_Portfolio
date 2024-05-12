using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderComboDetailDAO
    {
        private static OrderComboDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderComboDetailDAO() { }
        public static OrderComboDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderComboDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderComboDetail> GetAll()
        {
            List<OrderComboDetail> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = (from cb in DBContext.OrderComboDetails.Include(opd => opd.Order.User).Include(ocd => ocd.Order).Include(ocd => ocd.Combo).ToList()
                                                  select cb).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public OrderComboDetail GetById(int id)
        {
            OrderComboDetail result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = (OrderComboDetail)(from cb in DBContext.OrderComboDetails.Include(opd => opd.Order.User).Include(ocd => ocd.Order).Include(ocd => ocd.Combo).ToList()
                                                  where cb.OrderComboDetailId == id
                                                  select cb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public void Add(OrderComboDetail ocd)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.OrderComboDetails.Add(ocd);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteById(int id)
        {
            try
            {
                OrderComboDetail ocd = GetById(id);
                if (ocd != null)
                {
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.OrderComboDetails.Remove(ocd);
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrderComboDetail(OrderComboDetail cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.OrderComboDetails.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
   
}
