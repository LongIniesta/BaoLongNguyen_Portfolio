using BusinessObjects;
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
        public Category GetByID(int id)
        {
            Category result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Categories.SingleOrDefault(u => u.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Category AddCategory(Category Category)
        {
            Category result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Categories.Add(Category).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Category RemoveCategory(int id)
        {
            Category result;
            Category Category = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Categories.Remove(Category).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Category UpdateCategory(Category Category)
        {
            Category result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Categories.Update(Category).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> result = new List<Category>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
