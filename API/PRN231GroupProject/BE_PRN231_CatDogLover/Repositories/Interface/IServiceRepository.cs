using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IServiceRepository
    {
        Service GetByID(string id);
        Service AddService(Service Service);
        Service RemoveService(string id);
        Service UpdateService(Service Service);
        IEnumerable<Service> GetAll();
    }
}
