using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ComboDetailRepository : IComboDetailRepository
    {
        public void Create(ComboDetail cbd) => ComboDetailDAO.Instance.Create(cbd);

        public void DeleteById(int comboId, int productId) => ComboDetailDAO.Instance.DeleteById(comboId, productId);

        public List<ComboDetail> GetAll() => ComboDetailDAO.Instance.GetAll().ToList();

        public ComboDetail GetById(int comboId, int productId) => ComboDetailDAO.Instance.GetById(comboId, productId);
    }
}
