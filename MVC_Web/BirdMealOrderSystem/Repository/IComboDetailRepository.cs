using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IComboDetailRepository
    {
        List<ComboDetail> GetAll();
        void Create(ComboDetail cbd);
        void DeleteById(int comboId, int productId);
        ComboDetail GetById(int comboId, int productId);
    }
}
