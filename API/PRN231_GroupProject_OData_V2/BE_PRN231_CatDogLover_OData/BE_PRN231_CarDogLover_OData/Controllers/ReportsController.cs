using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Interface;
using Repositories;
using DTOs;
using Microsoft.AspNetCore.OData.Query;
using DataAccess;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;

namespace BE_PRN231_CarDogLover_OData.Controllers
{
    public class ReportsController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        private IPostRepository postRepository;
        private IReactRepository reactRepository;
        private IReactTypeRepository reactTypeRepository;
        private IReportRepository reportRepository;
        public ReportsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();
            accountRepository = new AccountRepository();
            productRepository = new ProductRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
            postRepository = new PostRepository();
            reactTypeRepository = new ReactTypeRepository();
            reactRepository = new ReactRepository();
            reportRepository = new ReportRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<ReportDTO>> GetReports()
        {
            List<ReportDTO> result;
            try
            {
                result = mapper.Map<List<ReportDTO>>(reportRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }


        [Authorize(Roles = "user")]
        [HttpPost("[controller]")]
        public ActionResult PostReport([FromBody] ReportDTO reportDTO)
        {
            if (reportDTO.ReportedPersonId == reportDTO.ReporterId) return BadRequest("Not reprot yourself");
            if (accountRepository.GetById(reportDTO.ReporterId) == null || accountRepository.GetById(reportDTO.ReportedPersonId) == null)
                return BadRequest("Not found account");
            if (reportRepository.GetByID(reportDTO.ReporterId, reportDTO.ReportedPersonId) != null) return BadRequest("Not report 2 time!");
            try
            {
                reportRepository.AddReport(mapper.Map<Report>(reportDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Created("", reportDTO);
        }

        [Authorize]
        [HttpPut("[controller]")]
        public ActionResult PutReport([FromBody] ReportDTO reportDTO)
        {
            if (reportDTO.ReportedPersonId == reportDTO.ReporterId) return BadRequest("Not reprot yourself");
            if (accountRepository.GetById(reportDTO.ReporterId) == null || accountRepository.GetById(reportDTO.ReportedPersonId) == null)
                return BadRequest("Not found account");
            if (reportRepository.GetByID(reportDTO.ReporterId, reportDTO.ReportedPersonId) == null) return BadRequest("Not found report to 2 update");
            try
            {
                reportRepository.UpdateReport(mapper.Map<Report>(reportDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Updated");
        }


        [Authorize(Policy = "AdminOrStaff")]
        [HttpDelete("[controller]")]
        public ActionResult DeleteReport(int ReporterId, int ReportedPersonId)
        {
            if (reportRepository.GetByID(ReporterId, ReportedPersonId) == null) return BadRequest("Not found report to delete");
            try
            {
                reportRepository.RemoveReport(ReporterId, ReportedPersonId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}
