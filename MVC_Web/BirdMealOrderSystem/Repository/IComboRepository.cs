using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IComboRepository
    {
        List<Combo> GetAll(bool status);
        Combo GetById(int id);
        void Create(Combo Combo);
        void DeleteById(int id);
        void Update(Combo Combo);
    }
}
