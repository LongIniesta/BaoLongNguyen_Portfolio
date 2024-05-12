using AutoMapper;
using BusinessObject;
using DTO;

namespace Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VoiceSeller, VoiceSellerDTO>().ReverseMap();
            CreateMap<VoiceSellerDTO, VoiceSellerRegisterDTO>().ReverseMap();
            CreateMap<Buyer, BuyerDTO>().ReverseMap();
            CreateMap<BuyerDTO, BuyerRegisterDTO>().ReverseMap();
            CreateMap<Page<VoiceSellerDTO>, Page<VoiceSeller>>().ReverseMap();
            CreateMap<Page<BuyerDTO>, Page<Buyer>>().ReverseMap();
            CreateMap<VoiceDetail, VoiceDetailDTO>().ReverseMap();
            CreateMap<Page<VoiceDetail>, Page<VoiceDetailDTO>>().ReverseMap();
            CreateMap<VoiceProperty, VoicePropertyDTO>().ReverseMap();
            CreateMap<VoiceType, VoiceTypeDTO>().ReverseMap();
            CreateMap<VoiceProject, VoiceProjectDTO>().ReverseMap();
            CreateMap<VoiceJob, VoiceDemoDTO>().ReverseMap();
            CreateMap<VoiceJob, VoiceJobDTO>().ReverseMap();
            CreateMap<Page<VoiceJob>, Page<VoiceJobDTO>>().ReverseMap();
            CreateMap<PaymentDetail, VoiceProject>().ReverseMap();
            CreateMap<Page<VoiceProject>, Page<VoiceProjectDTO>>().ReverseMap();
            CreateMap<SellerInAna, VoiceDetail>().ReverseMap();
            CreateMap<ProjectInAna, VoiceProject>().ReverseMap();
        }
    }

}
