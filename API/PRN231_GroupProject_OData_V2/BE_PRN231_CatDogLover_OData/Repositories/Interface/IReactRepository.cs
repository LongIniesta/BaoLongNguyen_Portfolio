using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IReactRepository
    {
        React GetByID(int AccountId, int PostId);
        React AddReact(React React);
        React RemoveReact(int AccountId,int PostId);
        React UpdateReact(React React);
        IEnumerable<React> GetAll();
    }
}
