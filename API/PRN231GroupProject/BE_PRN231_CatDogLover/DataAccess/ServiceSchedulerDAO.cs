using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ServiceSchedulerDAO
    {
        private static ServiceSchedulerDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceSchedulerDAO() { }
        public static ServiceSchedulerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceSchedulerDAO();
                    }
                    return instance;
                }
            }
        }
        public ServiceScheduler GetByID(string id, DateTime startDate)
        {
            ServiceScheduler result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ServiceSchedulers.SingleOrDefault(u => u.ServiceId == id && u.StartDate == startDate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public ServiceScheduler AddServiceScheduler(ServiceScheduler ServiceScheduler)
        {
            ServiceScheduler result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ServiceSchedulers.Add(ServiceScheduler).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public ServiceScheduler RemoveServiceScheduler(string id, DateTime startDate)
        {
            ServiceScheduler result;
            ServiceScheduler ServiceScheduler = GetByID(id, startDate);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ServiceSchedulers.Remove(ServiceScheduler).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public ServiceScheduler UpdateServiceScheduler(ServiceScheduler ServiceScheduler)
        {
            ServiceScheduler result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ServiceSchedulers.Update(ServiceScheduler).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<ServiceScheduler> GetAll()
        {
            List<ServiceScheduler> result = new List<ServiceScheduler>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.ServiceSchedulers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
