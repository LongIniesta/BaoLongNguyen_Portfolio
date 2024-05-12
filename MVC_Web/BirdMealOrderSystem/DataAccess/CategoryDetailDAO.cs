using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDetailDAO
    {
        private static CategoryDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDetailDAO() { }
        public static CategoryDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<CategoryDetail> GetAll()
        {
            List<CategoryDetail> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.CategoryDetails.ToList();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void AddCategoryDetail(CategoryDetail cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.CategoryDetails.Add(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategoryDetail(CategoryDetail cdt)
        {
           
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.CategoryDetails.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CategoryDetail getCategoryDetailById(int id)
        {
            CategoryDetail CategoryDetail = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                CategoryDetail = DBContext.CategoryDetails.FirstOrDefault(cb => cb.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return CategoryDetail;
        }
        public void DeleteCategoryDetailById(int id)
        {
            CategoryDetail CategoryDetail = null;
            CategoryDetail = getCategoryDetailById(id);
            try
            {
                if (CategoryDetail == null)
                {
                    return;
                }
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.CategoryDetails.Remove(CategoryDetail);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
