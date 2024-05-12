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
    public class ServiceRepository : IServiceRepository
    {
        public Service AddService(Service Service) => ServiceDAO.Instance.AddService(Service);

        public IEnumerable<Service> GetAll() => ServiceDAO.Instance.GetAll();

        public Service GetByID(string id) => ServiceDAO.Instance.GetByID(id);

        public Service RemoveService(string id) => ServiceDAO.Instance.RemoveService(id);

        public Service UpdateService(Service Service) => ServiceDAO.Instance.UpdateService(Service);
    }
}
