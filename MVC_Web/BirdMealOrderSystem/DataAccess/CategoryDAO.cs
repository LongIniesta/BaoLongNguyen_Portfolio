using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public void AddCategory(Category cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Categories.Add(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCategory(Category category)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Categories.Remove(category);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkExits(Category cat)
        {
            
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                if (DBContext.Categories.Any(c => c.ProductId == cat.ProductId && c.CategoryId == cat.CategoryId))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
    }
}
