using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetAll()
        {
            List<Product> result;
            try
            {
                var DBConetxt = new BirdMealOrderSystemContext();
                result = DBConetxt.Products.Include(p => p.Supplier).Include(p => p.Categories).ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Product GetById(int id) {
            Product result;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.Products.Include(p => p.Supplier).Include(p => p.Categories).FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Add(Product product)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Products.Add(product);
                DBContext.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                Product p = null;
                p = GetById(id);
                if (p != null)
                {
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.Products.Remove(p);
                    DBContext.SaveChanges();
                }
            }catch(Exception ex) { 
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(Product cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Products.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
