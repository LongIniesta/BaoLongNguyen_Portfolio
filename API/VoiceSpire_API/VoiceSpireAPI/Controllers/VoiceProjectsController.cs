using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using AutoMapper;
using DTO;
using Repository;
using Repository.Interface;
using DataAccess;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using Microsoft.Office.Interop.Word;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using NPOI.XWPF.UserModel;
using NPOI.XWPF.Extractor;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;

namespace VoiceSpireAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceProjectsController : ControllerBase
    {
        private IVoiceProjectRepository voiceProjectRepository;
        private IVoiceJobRepository voiceJobRepository;
        private IVoicePropertyRepository voicePropertyRepository;
        private IVoiceTypeRepository voiceTypeRepository;
        private IVoiceTransactionRepository voiceTransactionRepository;
        private IVoiceDetailRepository voiceDetailRepository;
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        public VoiceProjectsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            voiceJobRepository = new VoiceJobRepository();
            voiceDetailRepository = new VoiceDetailRepository();
            voiceTransactionRepository = new VoiceTransactionRepository();
            this.mapper = mapper;
            voicePropertyRepository = new VoicePropertyRepository();
            voiceTypeRepository = new VoiceTypeRepostiory();
            voiceProjectRepository = new VoiceProjectRepository();
        }

        [HttpGet("GetListProjectToManagement/{currentPage:int},{PageSize:int},{sortType}")]
        public async Task<ActionResult<Page<VoiceProjectDTO>>> GetListProjectManager(int currentPage, int PageSize, string? search, string sortType)
        {
            if (sortType != "new") sortType = "old";
            if (search == null) search = "";
            Page<VoiceProjectDTO> page = mapper.Map<Page<VoiceProjectDTO>>(voiceProjectRepository.SearchByFilter(currentPage, PageSize, search, 0,
             int.MaxValue, "", "", "", "", int.MaxValue, "", "", sortType)); 
            if (page == null) return NotFound("Not found any voice detail");
            return Ok(mapper.Map<Page<VoiceProjectDTO>>(page));

        }

        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult<VoiceProjectDTO>> GetByID(int id)
        {
            VoiceProject tmp = voiceProjectRepository.GetById(id);
            if (tmp == null) return NotFound();
            VoiceProjectDTO result = mapper.Map<VoiceProjectDTO>(tmp);
            return Ok(result);
        }
        [HttpGet("SearchByFilter/{currentPage:int},{PageSize:int}")]
        public async Task<ActionResult<Page<VoiceProjectDTO>>> SearchByFilter(int currentPage, int PageSize, string? search, string? sortType,
            int? fromPrice, int? toPrice, string? region, string? type, string? gender, string? property, int? duration)
        {
            if (sortType != "new") sortType = "old";
            if (search == null) search = "";
            if (fromPrice == null) fromPrice = 0;
            if (toPrice == null) toPrice = int.MaxValue;
            if (region == null) region = "";
            if (gender == null) gender = "";
            if (property == null) property = "";
            if (type == null) type = "";
            if (duration == null) duration = int.MaxValue;
            Page<VoiceProjectDTO> page = mapper.Map<Page<VoiceProjectDTO>>(voiceProjectRepository.SearchByFilter(currentPage, PageSize, search,(int) fromPrice,
             (int)toPrice, region, type, gender, property,(int) duration, "apply", "post", sortType));
            if (page == null) return NotFound("Not found any voice Project");
            return Ok(mapper.Map<Page<VoiceProjectDTO>>(page));

        }

        [HttpPut("ApproveProject/{id}")]
        public async Task<ActionResult> ApproveProject(int id)
        {
            VoiceProject voiceProject = voiceProjectRepository.GetById(id);
            if (voiceProject == null) return NotFound("Not found project");
            if (voiceProject.ProjectType.ToLower().Trim() == "send")
            {
                VoiceJob voiceJob = voiceJobRepository.GetByProjectIdForProjectSend(id);
                if (voiceJob == null) return NotFound("Not found project");
                voiceJob.Status = true;
                voiceJobRepository.UpdateVoiceJob(voiceJob);
                voiceProjectRepository.UpdateVoicePaymentStatus(id, "done");
                voiceProjectRepository.UpdateVoiceProjectStatus(id, "WaitToAccept"); 
            return Ok("Approved");
            } else {
                voiceProjectRepository.UpdateVoicePaymentStatus(id, "done");
                voiceProjectRepository.UpdateVoiceProjectStatus(id, "Apply");
            return Ok("Approved");
            }
        }



        [HttpPut("NotApprovedProject/{id}")]
        public async Task<ActionResult> NotApproveProject(int id)
        {
            if (voiceProjectRepository.UpdateVoiceProjectStatus(id, "NotApproved") != null) return Ok("NotApproved");
            return NotFound("Not found project");
        }

        [HttpGet("GetListDemoForProject/{projectId}")]
        public async Task<ActionResult<List<VoiceDemoDTO>>> GetListDemoForPorject(int projectId) {
            List<VoiceJob> resultTmp = voiceJobRepository.GetByProjectIdForProjectPost(projectId).ToList();
            if (resultTmp == null || resultTmp.Count == 0) {
                return BadRequest("Project not contain any demo");
            }

            List<VoiceDemoDTO> result = mapper.Map<List<VoiceDemoDTO>>(resultTmp);
            for (int i = 0; i < result.Count; i++) {
                result[i].VoiceDetail = mapper.Map<VoiceDetailDTO>(voiceDetailRepository.GetByID(result[i].VoiceSellerId));
                result[i].NumberOfBooking = voiceJobRepository.countNumberOfBooking(result[i].VoiceSellerId);
                List<VoiceType> listType = voiceTypeRepository.GetBySellerId(result[i].VoiceSellerId).ToList();
                if (listType != null && listType.Count() > 0)
                {
                    result[i].VoiceDetail.voiceTypes = mapper.Map<List<VoiceTypeDTO>>(listType);
                }
                List<VoiceProperty> listProperty = voicePropertyRepository.GetBySellerId(result[i].VoiceSellerId).ToList();
                if (listProperty != null && listProperty.Count() > 0)
                {
                    result[i].VoiceDetail.voiceProperties = mapper.Map<List<VoicePropertyDTO>>(listProperty);
                }
            }
            return Ok(result);
        }

        [HttpGet("GetTransactionOfProject/{projectId}")]
        public async Task<ActionResult<List<VoiceTransaction>>> GetTransactionOfProject(int projectId) { 
            List<VoiceTransaction> result = voiceTransactionRepository.GetByProjectId(projectId).ToList();
            if (result == null || result.Count <=0) return BadRequest("Not have any transaction");
            return result;
        }
        [HttpGet("GetPaymentInformation/{projectId}")]
        public async Task<ActionResult<BankInfomationDTO>> GetPaymentInfor(int projectId) {
            return voiceProjectRepository.GetPaymentInformationByProjectId(projectId);
        }

        [HttpGet("GetPaymentDetail/{projectId}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int projectId)
        {
            VoiceProject voiceProject = voiceProjectRepository.GetById(projectId);
            if (voiceProject == null) return BadRequest("Not found project");
            return mapper.Map<PaymentDetail>(voiceProject);
        }

        [HttpGet("SearchByFilterForManager/{currentPage},{PageSize},{sortType},{projectType},{WaitApprove},{NotApproved},{Apply},{Processing},{Done},{WaitToAccept},{Denied},")]
        public async Task<ActionResult<Page<VoiceProjectDTO>>> SearchByFilterForManager(int currentPage, int PageSize, string? search,
            bool WaitApprove, bool NotApproved, bool Apply, bool Processing, bool Done, bool WaitToAccept, bool Denied, string projectType, string sortType)
        {
            if (search == null) search = "";
            return Ok(mapper.Map<Page<VoiceProjectDTO>>(voiceProjectRepository.SearchProjectFilterForManager(currentPage, PageSize, search, WaitApprove, NotApproved, Apply, Processing, Done, WaitToAccept, Denied, projectType, sortType)));
        }

        [HttpPost("AIAnalysis")]
        public IActionResult ReadFile(IFormFile file)
        {
            string result;
            if (file == null || file.Length == 0)
            {
                return BadRequest("Vui lòng chọn một tệp.");
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase)) // Kiểm tra nếu là .txt
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var text = reader.ReadToEnd();
                    result = text;
                }
            }
            else if (fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase)) // Kiểm tra nếu là .doc
            {
                var doc = new XWPFDocument(file.OpenReadStream());
                XWPFWordExtractor extractor = new XWPFWordExtractor(doc);
                var text = extractor.Text;
                result = text;
            }
            else
            {
                return BadRequest("Phần mở rộng của tệp không được hỗ trợ.");
            }

            int spaceCount = result.Count(char.IsWhiteSpace);
            if (spaceCount > 850)
            return BadRequest("OpenAi chỉ hỗ trợ tối đa 850 từ trong văn bản!");

            try {
                var YOUR_API_KEY = "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"; //https://beta.openai.com/account/api-keys
                var userInput = "Dựa vào đoạn văn sau, trả lời theo mẫu, chọn 1 đáp án và không giải thích gì thêm:" +
                    "\r\n- Thể loại (chọn 1) : thuyết minh, quảng cáo, review, kể chuyện, thuyết trình, thời sự, thông báo" +
                    "\r\n- Giọng đọc (chọn 1): nam, nữ, cả hai\r\n- Tốc độ đọc: nhanh, chậm, vừa" +
                    "\r\n- Giọng miền (chọn 1): bắc, nam\r\n- Màu giọng (Chọn 1 hoặc 2): mạnh mẽ, trẻ trung, trung niên, tươi mới, hài hước, tự tin, trẻ em, dịu dàng" +
                    "\r\n- Tone giọng (chọn 1): thấp, vừa, cao\r\n- Truyền cảm: tốt, vừa phải\r\n (Dựa vào đề bài và trả lời theo mẫu và không giải thích gì thêm) " +
                    "(Mẫu: kể chuyện-nam-nhanh-bắc-mạnh mẽ-thấp-tốt)" +
                    " - Đoạn văn: " + result;
                var client = new RestClient("https://api.openai.com/v1");
                var request = new RestRequest("engines/text-davinci-003/completions", RestSharp.Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
                request.AddJsonBody(new { prompt = userInput, max_tokens = 200, temperature = 0 });
                var response = client.Execute(request);
                var responseData = JObject.Parse(response.Content);
                string output = responseData["choices"][0]["text"].ToString();

                return Ok(convertOutput(output));
            } catch (Exception ex)
            {
                return BadRequest("Phân tích không thành công, vui lòng thử lại");
            }
        }

        private ProjectAnalystRespone convertOutput(string s)
        {
            string[] arr = s.Split('-');
            var result = new ProjectAnalystRespone {
                VoiceType = arr[0],
                VoiceGender = arr[1],
                VoiceSpeed = arr[2],
                VoiceRegion = arr[3],
                VoiceProperty = arr[4],
                VoiceTone = arr[5],
                VoiceInspirational = arr[6],
                VoicePronouce = "tốt",
                VoiceStress = "tốt",
            };
            return result;
        }

        [HttpPost("ProjectSuggestions/{projectId}")]
        public ActionResult<List<ProjectAnaDTO>> Suggest(int projectId)
        {
            List<ProjectAnaDTO> result = new List<ProjectAnaDTO>();

            VoiceProject project = VoiceProjectDAO.Instance.GetByID(projectId);
            if (project == null) return BadRequest("ProjectId is invalid");
            List<VoiceDetail> voiceDetails = voiceDetailRepository.GetPage(1, 1000, "", "new", true).results;
            foreach (VoiceDetail voice in voiceDetails) {
                int subility = 0;
                ProjectAnaDTO projectAna = new ProjectAnaDTO();
                projectAna.voiceProjectId = projectId;
                projectAna.voiceDetailId = voice.VoiceDetailId;
                projectAna.SellerInAna = mapper.Map<SellerInAna>(voice);
                projectAna.projectInAna = mapper.Map<ProjectInAna>(project);
                //So sánh truyền cảm
                if (voice.VoiceInspirational < project.VoiceInspirational) {
                    subility += (20 - ( (int)project.VoiceInspirational - (int) voice.VoiceInspirational)*5);
                }
                if (voice.VoiceInspirational >= project.VoiceInspirational) subility += 20;
                //Tốc độ đọc
                if (voice.VoiceSpeed < project.VoiceSpeed)
                    {
                        subility += (15 - ((int)project.VoiceSpeed - (int)voice.VoiceSpeed) * 5);
                    }
                if (voice.VoiceSpeed == project.VoiceSpeed) subility += 15;
                if (voice.VoiceSpeed > project.VoiceSpeed)
                {
                    subility += (15 - ((int)voice.VoiceSpeed - (int)project.VoiceSpeed) * 5);
                }

                //Trọng âm
                if (voice.VoiceStress < project.VoiceStress)
                {
                    subility += (12 - ((int)project.VoiceStress - (int)voice.VoiceStress) * 3);
                }
                if (voice.VoiceStress >= project.VoiceStress) subility += 12;

                //Tính chất
                if (voicePropertyRepository.GetBySellerId(voice.VoiceSellerId).Select(v => v.VoicePropertyName.ToLower()).ToList().Any(v => v.Contains(project.VoiceProperty.ToLower()))) {
                    subility += 10;
                }
                //Tone giọng
                if (voice.VoiceTone < project.VoiceTone)
                {
                    subility += (10 - ((int)project.VoiceTone - (int)voice.VoiceTone) * 2);
                }
                if (voice.VoiceTone == project.VoiceTone) subility += 10;
                if (voice.VoiceTone > project.VoiceTone)
                {
                    subility += (10 - ((int)voice.VoiceTone - (int)project.VoiceTone) * 2);
                }
                //Vùng miền
                if (voice.VoiceRegion == project.VoiceRegion) subility += 8;

                //giới tính
                if (voice.VoiceGender == project.VoiceGender) subility += 15;

                //Phát âm
                if (voice.VoicePronouce < project.VoicePronouce)
                {
                    subility += (5 - ((int)project.VoicePronouce - (int)voice.VoicePronouce) * 1);
                }
                if (voice.VoicePronouce >= project.VoicePronouce) subility += 5;

                //Giá
                if (voice.Price <= project.Price) subility += 5;

                projectAna.Suitability = subility;
                result.Add(projectAna);
            }
            return Ok(result.OrderByDescending(r => r.Suitability));
        }
        [HttpPost("ProjectSuggestions/{projectId}/{sellerId}")]
        public ActionResult<ProjectAnaDTO> Suggest(int projectId, int sellerId)
        {
            VoiceProject project = VoiceProjectDAO.Instance.GetByID(projectId);
            if (project == null) return BadRequest("ProjectId is invalid");          
            VoiceDetail voice = voiceDetailRepository.GetByID(sellerId);
                int subility = 0;
                ProjectAnaDTO projectAna = new ProjectAnaDTO();
                projectAna.voiceProjectId = projectId;
                projectAna.voiceDetailId = voice.VoiceDetailId;
                projectAna.SellerInAna = mapper.Map<SellerInAna>(voice);
                projectAna.projectInAna = mapper.Map<ProjectInAna>(project);
                //So sánh truyền cảm
                if (voice.VoiceInspirational < project.VoiceInspirational)
                {
                    subility += (20 - ((int)project.VoiceInspirational - (int)voice.VoiceInspirational) * 5);
                }
                if (voice.VoiceInspirational >= project.VoiceInspirational) subility += 20;
                //Tốc độ đọc
                if (voice.VoiceSpeed < project.VoiceSpeed)
                {
                    subility += (15 - ((int)project.VoiceSpeed - (int)voice.VoiceSpeed) * 5);
                }
                if (voice.VoiceSpeed == project.VoiceSpeed) subility += 15;
                if (voice.VoiceSpeed > project.VoiceSpeed)
                {
                    subility += (15 - ((int)voice.VoiceSpeed - (int)project.VoiceSpeed) * 5);
                }

                //Trọng âm
                if (voice.VoiceStress < project.VoiceStress)
                {
                    subility += (12 - ((int)project.VoiceStress - (int)voice.VoiceStress) * 3);
                }
                if (voice.VoiceStress >= project.VoiceStress) subility += 12;

                //Tính chất
                if (voicePropertyRepository.GetBySellerId(voice.VoiceSellerId).Select(v => v.VoicePropertyName.ToLower()).ToList().Any(v => v.Contains(project.VoiceProperty.ToLower())))
                {
                    subility += 10;
                }
                //Tone giọng
                if (voice.VoiceTone < project.VoiceTone)
                {
                    subility += (10 - ((int)project.VoiceTone - (int)voice.VoiceTone) * 2);
                }
                if (voice.VoiceTone == project.VoiceTone) subility += 10;
                if (voice.VoiceTone > project.VoiceTone)
                {
                    subility += (10 - ((int)voice.VoiceTone - (int)project.VoiceTone) * 2);
                }
                //Vùng miền
                if (voice.VoiceRegion == project.VoiceRegion) subility += 8;

                //giới tính
                if (voice.VoiceGender == project.VoiceGender) subility += 15;

                //Phát âm
                if (voice.VoicePronouce < project.VoicePronouce)
                {
                    subility += (5 - ((int)project.VoicePronouce - (int)voice.VoicePronouce) * 1);
                }
                if (voice.VoicePronouce >= project.VoicePronouce) subility += 5;

                //Giá
                if (voice.Price <= project.Price) subility += 5;

                projectAna.Suitability = subility;
            
            return Ok(projectAna);
        }




    }
}
