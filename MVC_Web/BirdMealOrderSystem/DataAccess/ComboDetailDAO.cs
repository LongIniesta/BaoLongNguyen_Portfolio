using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ComboDetailDAO
    {
        private static ComboDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private ComboDetailDAO() { }
        public static ComboDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ComboDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ComboDetail> GetAll()
        {
            List<ComboDetail> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.ComboDetails.Include(cbd => cbd.Product).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Create(ComboDetail cbd)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Add(cbd);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteById(int comboId, int productId)
        {
            ComboDetail result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                 result = GetById(comboId, productId);
                if (result != null)
                {
                    DBContext.Remove(result);
                    DBContext.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ComboDetail GetById(int comboId, int productId)
        {
            ComboDetail result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.ComboDetails.Where(cbd => cbd.ComboId == comboId && cbd.ProductId == productId).ToList().SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
