using BusinessObjects;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ReportRepository : IReportRepository
    {
        public Report AddReport(Report Report) => ReportDAO.Instance.AddReport(Report);

        public IEnumerable<Report> GetAll() => ReportDAO.Instance.GetAll();

        public Report GetByID(int reporterId, int reportedPersonId) => ReportDAO.Instance.GetByID(reporterId, reportedPersonId);

        public Report RemoveReport(int reporterId, int reportedPersonId) => ReportDAO.Instance.RemoveReport(reporterId, reportedPersonId);

        public Report UpdateReport(Report Report) => ReportDAO.Instance.UpdateReport(Report);
    }
}
