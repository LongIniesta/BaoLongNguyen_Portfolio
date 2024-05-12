using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        // Using singleton pattern
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<User> GetAll()
        {
            List<User> result;
            try
            {
                var DBConetxt = new BirdMealOrderSystemContext();
                result = DBConetxt.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public User GetById(int id)
        {
            User result;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.Users.FirstOrDefault(p => p.UserId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Add(User User)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Users.Add(User);
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
                User p = null;
                p = GetById(id);
                if (p != null)
                {
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.Users.Remove(p);
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateUser(User cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Users.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
