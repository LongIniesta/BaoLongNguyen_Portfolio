using BusinessObject;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoiceSellerDAO
    {
        private static VoiceSellerDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceSellerDAO() { }
        public static VoiceSellerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceSellerDAO();
                    }
                    return instance;
                }
            }
        }

        public VoiceSeller GetByEmailAndPassword(string Email, string Password)
        {
            VoiceSeller result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.SingleOrDefault(u => u.Email.Equals(Email));
                if (result != null) {
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(Password, result.Password);
                    if (passwordMatch)
                    {
                        return result;
                    }
                    else {
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
        public VoiceSeller GetByID(int id)
        {
            VoiceSeller result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.SingleOrDefault(u => u.VoiceSellerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public VoiceSeller AddVoiceSeller(VoiceSeller VoiceSeller)
        {
            VoiceSeller result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.Add(VoiceSeller).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public VoiceSeller RemoveVoiceSeller(int id)
        {
            VoiceSeller result;
            VoiceSeller VoiceSeller = GetByID(id);
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.Remove(VoiceSeller).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceSeller UpdateVoiceSeller(VoiceSeller VoiceSeller)
        {
            VoiceSeller result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.Update(VoiceSeller).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<VoiceSeller> GetAll()
        {
            List<VoiceSeller> result = new List<VoiceSeller>();
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Page<VoiceSeller> GetPage(int currentPage, int PageSize, string search)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceSeller> result = new List<VoiceSeller>();
            Page<VoiceSeller> page = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                int TotalItems = DBContext.VoiceSellers.Count(u => u.Fullname.ToLower().Contains(search.ToLower().Trim())
                );
                int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                result = DBContext.VoiceSellers.Where(u => u.Fullname.ToLower().Contains(search.ToLower().Trim())
                )
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                page = new Page<VoiceSeller>
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

        public bool checkVoiceSellerExits(string Email)
        {
            bool result = true;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.Count(u => u.Email.Equals(Email)) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceSeller GetVoiceSellerByEmail(string Email) {
            VoiceSeller result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceSellers.SingleOrDefault(v => v.Email == Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public bool checkPaymentInfo(int sellerId)
        {
            bool result = true;
            try
            {
                var DBContext = new VoiceSpireContext();
                VoiceSeller seller = DBContext.VoiceSellers.SingleOrDefault(v => v.VoiceSellerId == sellerId);
                if (seller.BankName == null || seller.BankAccountName == null || seller.BankNumber == null
                    || seller.BankNumber.Trim() == "" || seller.BankAccountName.Trim() == "" || seller.BankName.Trim() == "")
                {
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
