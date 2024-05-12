using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReactDAO
    {
        private static ReactDAO instance = null;
        private static readonly object instanceLock = new object();
        private ReactDAO() { }
        public static ReactDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReactDAO();
                    }
                    return instance;
                }
            }
        }
        public React GetByID(int AccountId, int postId)
        {
            React result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reacts.SingleOrDefault(u => u.AccountId == AccountId && u.PostId == postId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public React AddReact(React React)
        {
            React result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reacts.Add(React).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public React RemoveReact(int AccountId, int postId)
        {
            React result;
            React React = GetByID(AccountId, postId);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reacts.Remove(React).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public React UpdateReact(React React)
        {
            React result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reacts.Update(React).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<React> GetAll()
        {
            List<React> result = new List<React>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reacts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
