using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();
        Supplier GetById(int id);
        void Create(Supplier Supplier);
        void DeleteById(int id);
        void Update(Supplier Supplier);
    }
}
