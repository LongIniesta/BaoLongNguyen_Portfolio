using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IServiceSchedulerRepository
    {
        ServiceScheduler GetByID(string id, DateTime startDate);
        ServiceScheduler AddServiceScheduler(ServiceScheduler ServiceScheduler);
        ServiceScheduler RemoveServiceScheduler(string id, DateTime startDate);
        ServiceScheduler UpdateServiceScheduler(ServiceScheduler ServiceScheduler);
        IEnumerable<ServiceScheduler> GetAll();
    }
}
