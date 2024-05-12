using BusinessObject;
using DTO;

namespace DataAccess
{
    public class BuyerDAO
    {
        private static BuyerDAO instance = null;
        private static readonly object instanceLock = new object();
        private BuyerDAO() { }
        public static BuyerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BuyerDAO();
                    }
                    return instance;
                }
            }
        }

        public Buyer GetByEmailAndPassword(string Email, string Password)
        {
            Buyer result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.SingleOrDefault(u => u.Email.Equals(Email));
                if (result != null)
                {
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(Password, result.Password);
                    if (passwordMatch)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Buyer GetByID(int id)
        {
            Buyer result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.SingleOrDefault(u => u.BuyerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public Buyer AddBuyer(Buyer Buyer)
        {
            Buyer result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.Add(Buyer).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

        public Buyer RemoveBuyer(int id)
        {
            Buyer result;
            Buyer Buyer = GetByID(id);
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.Remove(Buyer).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Buyer UpdateBuyer(Buyer Buyer)
        {
            Buyer result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.Update(Buyer).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Buyer> GetAll()
        {
            List<Buyer> result = new List<Buyer>();
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Page<Buyer> GetPage(int currentPage, int PageSize, string search)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<Buyer> result = new List<Buyer>();
            Page<Buyer> page = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                int TotalItems = DBContext.Buyers.Count(u => u.Fullname.ToLower().Contains(search.ToLower().Trim())
                );
                int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                result = DBContext.Buyers.Where(u => u.Fullname.ToLower().Contains(search.ToLower().Trim())
                )
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                page = new Page<Buyer>
                {
                    results = result,
                    Count = result.Count(),
                    PageIndex = currentPage,
                    TotalCount = TotalItems,
                    TotalPages = TotalPages,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return page;
        }

        public bool checkBuyerExits(string Email)
        {
            bool result = true;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.Count(u => u.Email.Equals(Email)) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Buyer GetBuyerByEmail(string Email)
        {
            Buyer result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.Buyers.SingleOrDefault(v => v.Email == Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Buyer> GetBuyerByName(string name)
        {
            List<Buyer> result = new List<Buyer>();
            try
            {
                using (var dbContext = new VoiceSpireContext())
                {
                    result = dbContext.Buyers
                        .Where(b => b.Fullname.Contains(name))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public bool checkPaymentInfo(int buyerId)
        {
            bool result = true;
            try
            {
                var DBContext = new VoiceSpireContext();
                Buyer buyer = DBContext.Buyers.SingleOrDefault(v => v.BuyerId == buyerId);
                if (buyer.BankName == null || buyer.BankAccountName == null || buyer.BankNumber == null
                    || buyer.BankNumber.Trim() == "" || buyer.BankAccountName.Trim() == "" || buyer.BankName.Trim() == "") {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
