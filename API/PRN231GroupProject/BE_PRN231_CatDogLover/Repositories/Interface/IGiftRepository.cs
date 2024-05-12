using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IGiftRepository
    {
        Gift GetByID(string id);
        Gift AddGift(Gift Gift);
        Gift RemoveGift(string id);
        Gift UpdateGift(Gift Gift);
        IEnumerable<Gift> GetAll();
    }
}
