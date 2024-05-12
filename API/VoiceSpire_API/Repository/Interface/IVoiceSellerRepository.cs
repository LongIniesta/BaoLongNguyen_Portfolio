using BusinessObject;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVoiceSellerRepository
    {
        VoiceSeller GetByEmailAndPassword(string Email, string Password);
        VoiceSeller GetByID(int id);
        VoiceSeller AddVoiceSeller(VoiceSeller VoiceSeller);
        VoiceSeller RemoveVoiceSeller(int id);
        VoiceSeller UpdateVoiceSeller(VoiceSeller VoiceSeller);
        IEnumerable<VoiceSeller> GetAll();
        Page<VoiceSeller> GetPage(int currentPage, int PageSize, string search);
        bool checkVoiceSellerExits(string Email);
        VoiceSeller getByEmail(string Email);
        bool checkPaymentInfo(int sellerId);
    }
}
