using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        public void Create(Supplier Supplier) => SupplierDAO.Instance.Add(Supplier);

        public void DeleteById(int id) => SupplierDAO.Instance.DeleteById(id);

        public List<Supplier> GetAll() => SupplierDAO.Instance.GetAll().ToList();

        public Supplier GetById(int id) => SupplierDAO.Instance.GetById(id);

        public void Update(Supplier Supplier) => SupplierDAO.Instance.UpdateSupplier(Supplier);
    }
}
