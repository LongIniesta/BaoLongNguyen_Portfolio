using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoleDAO() { }
        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }
        public Role GetByID(int id)
        {
            Role result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Roles.SingleOrDefault(u => u.RoleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Role AddRole(Role Role)
        {
            Role result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Roles.Add(Role).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Role RemoveRole(int id)
        {
            Role result;
            Role Role = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Roles.Remove(Role).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Role UpdateRole(Role Role)
        {
            Role result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Roles.Update(Role).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Role> GetAll()
        {
            List<Role> result = new List<Role>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
