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
    public class ReactTypeRepository : IReactTypeRepository
    {
        public ReactType AddReactType(ReactType ReactType) => ReactTypeDAO.Instance.AddReactType(ReactType);

        public IEnumerable<ReactType> GetAll() => ReactTypeDAO.Instance.GetAll();

        public ReactType GetByID(int id) => ReactTypeDAO.Instance.GetByID(id);


        public ReactType RemoveReactType(int id) => ReactTypeDAO.Instance.RemoveReactType(id);

        public ReactType UpdateReactType(ReactType ReactType) => ReactTypeDAO.Instance.UpdateReactType(ReactType);
    }
}
