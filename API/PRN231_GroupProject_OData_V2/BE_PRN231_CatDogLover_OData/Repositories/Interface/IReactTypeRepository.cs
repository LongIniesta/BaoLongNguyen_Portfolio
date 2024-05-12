using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IReactTypeRepository
    {
        ReactType GetByID(int id);
        ReactType AddReactType(ReactType ReactType);
        ReactType RemoveReactType(int id);
        ReactType UpdateReactType(ReactType ReactType);
        IEnumerable<ReactType> GetAll();
    }
}
