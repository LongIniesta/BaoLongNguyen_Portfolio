using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderProductDetailDAO
    {
        private static OrderProductDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderProductDetailDAO() { }
        public static OrderProductDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderProductDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<OrderProductDetail> GetAll()
        {
            List<OrderProductDetail> result;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.OrderProductDetails.Include(odp => odp.Product).Include(odp => odp.Order).Include(opd => opd.Order.User).ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public OrderProductDetail GetById(int id) {
            OrderProductDetail result;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.OrderProductDetails.Include(opd => opd.Product).Include(opd => opd.Order).Where(opd => opd.OrderProductDetailId == id).ToList().FirstOrDefault();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public List<OrderProductDetail> GetByProductId(int productId)
        {   
            List<OrderProductDetail> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.OrderProductDetails.Include(opd => opd.Order.User).Include(opd => opd.Product).Include(opd => opd.Order).Where(opd => opd.ProductId == productId).ToList();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Add(OrderProductDetail opd)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.OrderProductDetails.Add(opd);
                DBContext.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteByid(int id)
        {
            try
            {
                OrderProductDetail opd = null;
                opd = GetById(id);
                if (opd != null){
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.OrderProductDetails.Remove(opd);
                    DBContext.SaveChanges();
                }
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrderProductDetail(OrderProductDetail cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.OrderProductDetails.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
