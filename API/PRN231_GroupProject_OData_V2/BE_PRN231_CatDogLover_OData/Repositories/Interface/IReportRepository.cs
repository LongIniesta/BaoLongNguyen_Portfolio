using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IReportRepository
    {
        Report GetByID(int reporterId, int reportedPersonId);
        Report AddReport(Report Report);
        Report RemoveReport(int reporterId, int reportedPersonId);
        Report UpdateReport(Report Report);
        IEnumerable<Report> GetAll();
    }
}
