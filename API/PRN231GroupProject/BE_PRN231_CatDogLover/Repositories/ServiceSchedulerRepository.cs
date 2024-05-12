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
    public class ServiceSchedulerRepository : IServiceSchedulerRepository
    {
        public ServiceScheduler AddServiceScheduler(ServiceScheduler ServiceScheduler) {
            string ItemId = ItemDAO.Instance.AddItem(new Item { ItemType = "scheduler" }).ItemId;
            ServiceScheduler.ItemId = ItemId;
            return ServiceSchedulerDAO.Instance.AddServiceScheduler(ServiceScheduler);
        }

        public IEnumerable<ServiceScheduler> GetAll() => ServiceSchedulerDAO.Instance.GetAll(); 

        public ServiceScheduler GetByID(string id, DateTime startDate) => ServiceSchedulerDAO.Instance.GetByID(id, startDate);

        public ServiceScheduler RemoveServiceScheduler(string id, DateTime startDate) => ServiceSchedulerDAO.Instance.RemoveServiceScheduler(id, startDate);

        public ServiceScheduler UpdateServiceScheduler(ServiceScheduler ServiceScheduler) => ServiceSchedulerDAO.Instance.UpdateServiceScheduler(ServiceScheduler);
    }
}
