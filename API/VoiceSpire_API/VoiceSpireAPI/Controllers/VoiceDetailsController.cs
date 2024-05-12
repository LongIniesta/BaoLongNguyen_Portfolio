using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using AutoMapper;
using Repository.Interface;
using Repository;
using DTO;
using DataAccess;

namespace VoiceSpireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceDetailsController : ControllerBase
    {
        private IVoiceSellerRepository voiceSellerRepository;
        private IBuyerRepository buyerRepository;
        private IVoiceDetailRepository voiceDetailRepository;
        private IVoicePropertyRepository voicePropertyRepository;
        private IVoiceTypeRepository voiceTypeRepository;
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        public VoiceDetailsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            voiceSellerRepository = new VoiceSellerRepository();
            buyerRepository = new BuyerRepository();
            voiceDetailRepository = new VoiceDetailRepository();
            voicePropertyRepository = new VoicePropertyRepository();
            voiceTypeRepository = new VoiceTypeRepostiory();
            this.mapper = mapper;
        }
        [HttpGet("{sellerId}")]
        public async Task<ActionResult<VoiceDetailDTO>> GetVoiceDetail(int sellerId)
        {
            VoiceDetail voiceDetail = voiceDetailRepository.GetByID(sellerId);
            VoiceDetailDTO voiceDetailDTO = mapper.Map<VoiceDetailDTO>(voiceDetail);
            List<VoiceType> listType = voiceTypeRepository.GetBySellerId(sellerId).ToList();
            if (listType!= null && listType.Count() > 0)
            {
                voiceDetailDTO.voiceTypes = mapper.Map<List<VoiceTypeDTO>>(listType);
            }
            List<VoiceProperty> listProperty = voicePropertyRepository.GetBySellerId(sellerId).ToList();
            if (listProperty != null && listProperty.Count() > 0)
            {
                voiceDetailDTO.voiceProperties = mapper.Map<List<VoicePropertyDTO>>(listProperty);
            }
            if (voiceDetail == null) return NotFound("Seller doesn't have voice detail");
            return Ok(voiceDetailDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostVoiceDetail(VoiceDetailDTO voiceDetailDTO)
        { 
            if (voiceDetailRepository.GetByID(voiceDetailDTO.VoiceSellerId) == null)
            {
                return BadRequest("This seller not upload voice");
            }
            if (voiceDetailRepository.GetByID(voiceDetailDTO.VoiceSellerId).IsApprove == true) {
                return BadRequest("Voice detail has been approved already");
            }
            try {
                VoiceDetailDTO voiceDetailDTOpost = mapper.Map<VoiceDetailDTO>(voiceDetailDTO);
                voiceDetailDTOpost.IsApprove = true;
                voiceDetailDTOpost.VoiceGender = voiceDetailDTO.VoiceGender;
                voiceDetailDTOpost.VoiceLocal = voiceDetailDTO.VoiceLocal;
                voiceDetailDTOpost.VoiceInspirational = voiceDetailDTO.VoiceInspirational;
                voiceDetailDTOpost.VoicePronouce = voiceDetailDTO.VoicePronouce;
                voiceDetailDTOpost.VoiceTone = voiceDetailDTO.VoiceTone;
                voiceDetailDTOpost.VoiceRegion = voiceDetailDTO.VoiceRegion;
                voiceDetailDTOpost.VoiceSpeed = voiceDetailDTO.VoiceSpeed;
                voiceDetailDTOpost.VoiceStress = voiceDetailDTO.VoiceStress;
                foreach (VoicePropertyDTO voicePropertyDTO in voiceDetailDTO.voiceProperties)
                {
                    voicePropertyRepository.Add(mapper.Map<VoiceProperty>(voicePropertyDTO));
                }
                foreach (VoiceTypeDTO voiceTypeDTO in voiceDetailDTO.voiceTypes)
                {
                    voiceTypeRepository.Add(mapper.Map<VoiceType>(voiceTypeDTO));
                }
                voiceDetailRepository.UpdateVoiceDetail(mapper.Map<VoiceDetail>(voiceDetailDTO));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Approved");
        }
        [HttpGet("{currentPage:int},{PageSize:int},{sortType},{isApproved:bool}/GetPage")]
        public async Task<ActionResult<Page<VoiceDetailDTO>>> GetPage(int currentPage, int PageSize, string? search, string sortType, bool isApproved)
        {
            if (sortType == "new" || sortType == "old")
            {
                if (search == null) search = "";
                Page<VoiceDetail> pageTmp = voiceDetailRepository.GetPage(currentPage, PageSize, search, sortType, isApproved);
                if (pageTmp == null) return NotFound("Not found any voice detail");
                Page<VoiceDetailDTO> page = mapper.Map<Page<VoiceDetailDTO>>(pageTmp);
                for (int i = 0; i < page.results.Count(); i++) {
                    List<VoiceType> listType = voiceTypeRepository.GetBySellerId(page.results[i].VoiceSellerId).ToList();
                    if (listType != null && listType.Count() > 0)
                    {
                        page.results[i].voiceTypes = mapper.Map<List<VoiceTypeDTO>>(listType);
                    }
                    List<VoiceProperty> listProperty = voicePropertyRepository.GetBySellerId(page.results[i].VoiceSellerId).ToList();
                    if (listProperty != null && listProperty.Count() > 0)
                    {
                        page.results[i].voiceProperties = mapper.Map<List<VoicePropertyDTO>>(listProperty);
                    }
                }
                return Ok(page);
            }
            else return BadRequest("sortType invalid");
            
        }
        [HttpGet("{currentPage:int},{PageSize:int},{isApproved:bool}/SearchByFilter")]
        public async Task<ActionResult<Page<VoiceDetailDTO>>> SearchByFilter(int currentPage, int PageSize, string? search, 
            bool isApproved, int? fromPrice, 
            int? toPrice, int? tone, string? region, string? gender, string? property, int? rate, string? type)
        {
                if (search == null) search = "";
            if (fromPrice == null) fromPrice = 0;
            if (toPrice == null) toPrice = int.MaxValue;
            if (tone == null) tone = 0;
            if (region == null) region = "";
            if (gender == null) gender = "";
            if (property == null) property = "";
            if (type == null) type = "";
            if (rate == null) rate = 0;
            Page<VoiceDetail> pageTmp = voiceDetailRepository.SearchByFilter(currentPage, PageSize, search, isApproved,(int) fromPrice,
         (int)toPrice, (int)tone, region, gender, property,(int) rate, type);
                if (pageTmp == null) return NotFound("Not found any voice detail");
            Page<VoiceDetailDTO> page = mapper.Map<Page<VoiceDetailDTO>>(pageTmp);
            for (int i = 0; i < page.results.Count(); i++)
            {
                List<VoiceType> listType = voiceTypeRepository.GetBySellerId(page.results[i].VoiceSellerId).ToList();
                if (listType != null && listType.Count() > 0)
                {
                    page.results[i].voiceTypes = mapper.Map<List<VoiceTypeDTO>>(listType);
                }
                List<VoiceProperty> listProperty = voicePropertyRepository.GetBySellerId(page.results[i].VoiceSellerId).ToList();
                if (listProperty != null && listProperty.Count() > 0)
                {
                    page.results[i].voiceProperties = mapper.Map<List<VoicePropertyDTO>>(listProperty);
                }
            }
            return Ok(page);

        }
    }
}
