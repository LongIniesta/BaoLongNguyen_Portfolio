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
    public class VoiceDetailRepository : IVoiceDetailRepository
    {
        public VoiceDetail AddVoiceDetail(VoiceDetail VoiceDetail) => VoiceDetailDAO.Instance.AddVoiceDetail(VoiceDetail);

        public VoiceDetail GetByID(int voiceSellerId) => VoiceDetailDAO.Instance.GetByID(voiceSellerId);

        public Page<VoiceDetail> GetPage(int currentPage, int PageSize, string search, string sortType, bool isApproved) => VoiceDetailDAO.Instance.GetPage(currentPage,PageSize,search,sortType,isApproved);

        public Page<VoiceDetail> SearchByFilter(int currentPage, int PageSize, string search, bool isApproved, int fromPrice, int toPrice, int tone, string region, string gender, string property, int rate, string type)
        => VoiceDetailDAO.Instance.SearchByFilter(currentPage, PageSize, search, isApproved, fromPrice,
             toPrice, tone, region, gender, property, rate, type);

        public VoiceDetail UpdateVoiceDetail(VoiceDetail VoiceDetail) => VoiceDetailDAO.Instance.UpdateVoiceDetail(VoiceDetail);

    }
}
