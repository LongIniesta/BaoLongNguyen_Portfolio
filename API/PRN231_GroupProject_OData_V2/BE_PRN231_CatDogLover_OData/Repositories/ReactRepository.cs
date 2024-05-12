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
    public class ReactRepository : IReactRepository
    {
        public React AddReact(React React) => ReactDAO.Instance.AddReact(React);

        public IEnumerable<React> GetAll() => ReactDAO.Instance.GetAll();

        public React GetByID(int AccountId, int PostId) => ReactDAO.Instance.GetByID(AccountId, PostId);

        public React RemoveReact(int AccountId, int PostId) => ReactDAO.Instance.RemoveReact(AccountId, PostId);

        public React UpdateReact(React React) => ReactDAO.Instance.UpdateReact(React);
    }
}
