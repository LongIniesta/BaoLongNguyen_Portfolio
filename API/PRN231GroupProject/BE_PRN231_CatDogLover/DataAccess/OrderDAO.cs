using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public Order GetByID(int id)
        {
            Order result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Orders.SingleOrDefault(u => u.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Order AddOrder(Order Order)
        {
            Order result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Orders.Add(Order).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Order RemoveOrder(int id)
        {
            Order result;
            Order Order = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Orders.Remove(Order).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Order UpdateOrder(Order Order)
        {
            Order result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Orders.Update(Order).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> result = new List<Order>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
