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
    public class GiftRepository : IGiftRepository
    {
        public Gift AddGift(Gift Gift) => GiftDAO.Instance.AddGift(Gift);

        public IEnumerable<Gift> GetAll() => GiftDAO.Instance.GetAll();

        public Gift GetByID(string id) => GiftDAO.Instance.GetByID(id);

        public Gift RemoveGift(string id) => GiftDAO.Instance.RemoveGift(id);

        public Gift UpdateGift(Gift Gift) => GiftDAO.Instance.UpdateGift(Gift);
    }
}
