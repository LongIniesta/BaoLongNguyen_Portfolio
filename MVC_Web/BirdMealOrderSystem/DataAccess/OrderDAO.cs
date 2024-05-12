using BussinessObject;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Order> GetAll()
        {
            List<Order> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = (List<Order>)DBContext.Orders.Include(od => od.User).ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Order GetById(int id) {
            Order result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = (Order)DBContext.Orders.Include(od => od.User).FirstOrDefault(od => od.OrderId == id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Add(Order order)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Orders.Add(order);
                DBContext.SaveChanges();
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                Order od = null;
                od = GetById(id);
                if (od != null)
                {
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.Orders.Remove(od);
                    DBContext.SaveChanges();
                }
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrder(Order cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Orders.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
