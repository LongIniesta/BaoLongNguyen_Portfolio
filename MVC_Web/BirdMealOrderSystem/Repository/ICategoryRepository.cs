using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        void addCategory(Category cat);
        void removeCategory(Category cat);

        bool checkExits(Category cat);
    }
}
