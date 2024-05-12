using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public void Create(User User) => UserDAO.Instance.Add(User);

        public void DeleteById(int id) => UserDAO.Instance.DeleteById(id);

        public List<User> GetAll() => UserDAO.Instance.GetAll().ToList(); 

        public User GetById(int id) => UserDAO.Instance.GetById(id);

        public void Update(User User) => UserDAO.Instance.UpdateUser(User);
    }
}
