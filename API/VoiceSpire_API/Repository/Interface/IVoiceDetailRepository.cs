using BusinessObject;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVoiceDetailRepository
    {
        VoiceDetail GetByID(int voiceSellerId);
        VoiceDetail AddVoiceDetail(VoiceDetail VoiceDetail);
        VoiceDetail UpdateVoiceDetail(VoiceDetail VoiceDetail);
        Page<VoiceDetail> GetPage(int currentPage, int PageSize, string search, string sortType, bool isApproved);
        Page<VoiceDetail> SearchByFilter(int currentPage, int PageSize, string search, bool isApproved, int fromPrice,
            int toPrice, int tone, string region, string gender, string property, int rate, string type);
    }
}
