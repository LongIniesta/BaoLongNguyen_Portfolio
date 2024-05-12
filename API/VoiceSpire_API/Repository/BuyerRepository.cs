using BusinessObject;
using DataAccess;
using DTO;
using Repository.Interface;

namespace Repository
{
    public class BuyerRepository : IBuyerRepository
    {
        public Buyer AddBuyer(Buyer Buyer) => BuyerDAO.Instance.AddBuyer(Buyer);

        public bool checkBuyerExits(string Email) => BuyerDAO.Instance.checkBuyerExits(Email);

        public bool checkPaymentInfo(int buyerId) => BuyerDAO.Instance.checkPaymentInfo(buyerId);

        public IEnumerable<Buyer> GetAll() => BuyerDAO.Instance.GetAll();

        public IEnumerable<Buyer> GetBuyerByName(string name) => BuyerDAO.Instance.GetBuyerByName(name);

        public Buyer getByEmail(string Email) => BuyerDAO.Instance.GetBuyerByEmail(Email);

        public Buyer GetByEmailAndPassword(string Email, string Password) => BuyerDAO.Instance.GetByEmailAndPassword(Email, Password);

        public Buyer GetByID(int id) => BuyerDAO.Instance.GetByID(id);

        public Page<Buyer> GetPage(int currentPage, int PageSize, string search) => BuyerDAO.Instance.GetPage(currentPage, PageSize, search);

        public Buyer RemoveBuyer(int id) => BuyerDAO.Instance.RemoveBuyer(id);

        public Buyer UpdateBuyer(Buyer Buyer) => BuyerDAO.Instance.UpdateBuyer(Buyer);
    }
}
