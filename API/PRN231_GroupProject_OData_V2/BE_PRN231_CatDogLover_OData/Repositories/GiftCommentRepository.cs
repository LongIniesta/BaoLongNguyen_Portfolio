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
    public class GiftCommentRepository : IGiftCommentRepository
    {
        public GiftComment AddGiftComment(GiftComment GiftComment) => GiftCommentDAO.Instance.AddGiftComment(GiftComment);

        public IEnumerable<GiftComment> GetAll() => GiftCommentDAO.Instance.GetAll();

        public GiftComment RemoveGiftComment(int id) => GiftCommentDAO.Instance.RemoveGiftComment(id);

        public GiftComment UpdateGiftComment(GiftComment GiftComment) => GiftCommentDAO.Instance.UpdateGiftComment(GiftComment);
    }
}
