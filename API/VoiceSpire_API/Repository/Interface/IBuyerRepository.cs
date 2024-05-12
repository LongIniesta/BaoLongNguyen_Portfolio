using BusinessObject;
using DTO;

namespace Repository.Interface
{
    public interface IBuyerRepository
    {
        Buyer GetByEmailAndPassword(string Email, string Password);
        Buyer GetByID(int id);
        Buyer AddBuyer(Buyer Buyer);
        Buyer RemoveBuyer(int id);
        Buyer UpdateBuyer(Buyer Buyer);
        IEnumerable<Buyer> GetAll();
        Page<Buyer> GetPage(int currentPage, int PageSize, string search);
        bool checkBuyerExits(string Email);
        Buyer getByEmail(string Email);
        IEnumerable<Buyer> GetBuyerByName(string name);
        bool checkPaymentInfo(int buyerId);
    }
}
