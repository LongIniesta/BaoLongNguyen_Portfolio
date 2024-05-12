using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object instanceLock = new object();
        private ReportDAO() { }
        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }
        public Report GetByID(int reporterId, int reportedPersonId)
        {

            Report result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reports.SingleOrDefault(u => u.ReportedPersonId == reportedPersonId && u.ReporterId == reporterId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Report AddReport(Report Report)
        {
            Report result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reports.Add(Report).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Report RemoveReport(int reporterId, int reportedPersonId)
        {
            Report result;
            Report Report = GetByID(reporterId, reportedPersonId);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reports.Remove(Report).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Report UpdateReport(Report Report)
        {
            Report result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reports.Update(Report).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Report> GetAll()
        {
            List<Report> result = new List<Report>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Reports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
