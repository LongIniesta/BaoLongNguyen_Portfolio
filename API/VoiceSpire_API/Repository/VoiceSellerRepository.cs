using BusinessObject;
using DataAccess;
using DTO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VoiceSellerRepository : IVoiceSellerRepository
    {
        public VoiceSeller AddVoiceSeller(VoiceSeller VoiceSeller) => VoiceSellerDAO.Instance.AddVoiceSeller(VoiceSeller);

        public bool checkPaymentInfo(int sellerId) => VoiceSellerDAO.Instance.checkPaymentInfo(sellerId);

        public bool checkVoiceSellerExits(string Email) => VoiceSellerDAO.Instance.checkVoiceSellerExits(Email);

        public IEnumerable<VoiceSeller> GetAll() => VoiceSellerDAO.Instance.GetAll();

        public VoiceSeller getByEmail(string Email) => VoiceSellerDAO.Instance.GetVoiceSellerByEmail(Email);

        public VoiceSeller GetByEmailAndPassword(string Email, string Password) => VoiceSellerDAO.Instance.GetByEmailAndPassword(Email, Password);

        public VoiceSeller GetByID(int id) => VoiceSellerDAO.Instance.GetByID(id);

        public Page<VoiceSeller> GetPage(int currentPage, int PageSize, string search) => VoiceSellerDAO.Instance.GetPage(currentPage, PageSize, search);

        public VoiceSeller RemoveVoiceSeller(int id) => VoiceSellerDAO.Instance.RemoveVoiceSeller(id);

        public VoiceSeller UpdateVoiceSeller(VoiceSeller VoiceSeller) => VoiceSellerDAO.Instance.UpdateVoiceSeller(VoiceSeller);
    }
}
