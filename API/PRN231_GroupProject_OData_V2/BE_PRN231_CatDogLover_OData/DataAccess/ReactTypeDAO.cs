using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReactTypeDAO
    {
        private static ReactTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        private ReactTypeDAO() { }
        public static ReactTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReactTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public ReactType GetByID(int id)
        {
            ReactType result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ReactTypes.SingleOrDefault(u => u.ReactTypeId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public ReactType AddReactType(ReactType ReactType)
        {
            ReactType result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ReactTypes.Add(ReactType).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public ReactType RemoveReactType(int id)
        {
            ReactType result;
            ReactType ReactType = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ReactTypes.Remove(ReactType).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public ReactType UpdateReactType(ReactType ReactType)
        {
            ReactType result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ReactTypes.Update(ReactType).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<ReactType> GetAll()
        {
            List<ReactType> result = new List<ReactType>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ReactTypes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
