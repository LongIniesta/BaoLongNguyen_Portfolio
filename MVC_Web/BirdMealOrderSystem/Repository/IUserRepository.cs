using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        void Create(User User);
        void DeleteById(int id);
        void Update(User User);
    }
}
