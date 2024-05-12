using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ComboRepository : IComboRepository
    {
        public void Create(Combo Combo) => ComboDAO.Instance.CreateCombo(Combo);

        public void DeleteById(int id) => ComboDAO.Instance.DeleteComboById(id);

        public List<Combo> GetAll(bool status) => ComboDAO.Instance.GetAllByStatus(status).ToList();

        public Combo GetById(int id) => ComboDAO.Instance.getComboById(id);

        public void Update(Combo Combo) => ComboDAO.Instance.UpdateCombo(Combo);
    }
}
