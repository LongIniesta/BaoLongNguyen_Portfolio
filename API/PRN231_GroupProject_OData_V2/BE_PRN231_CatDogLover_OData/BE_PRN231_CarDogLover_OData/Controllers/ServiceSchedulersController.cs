using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Interface;
using Repositories;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class ServiceSchedulersController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        private IServiceRepository serviceRepository;
        private IPostRepository postRepository;
        public ServiceSchedulersController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();
            accountRepository = new AccountRepository();
            productRepository = new ProductRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
            serviceRepository = new ServiceRepository();
            postRepository = new PostRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<ServiceSchedulerDTO>> GetServiceSchedulers()
        {
            List<ServiceSchedulerDTO> result;
            try
            {
                result = mapper.Map<List<ServiceSchedulerDTO>>(serviceSchedulerRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize]
        [HttpPut("[controller]")]
        public ActionResult UpdateServiceScheduler([FromBody] ServiceSchedulerDTO serviceSchedulerDTO)
        {
            ServiceScheduler svc = serviceSchedulerRepository.GetAll().SingleOrDefault(p => p.ItemId == serviceSchedulerDTO.ItemId && p.ServiceId == serviceSchedulerDTO.ServiceId);
            if (svc == null) return BadRequest("Not found service scheduler to update");
            if (svc.Status == false || svc.Service.Status == false) return BadRequest("Not found product to update");
            if (serviceSchedulerDTO.StartDate < DateTime.Now || serviceSchedulerDTO.StartDate >= serviceSchedulerDTO.EndDate) return BadRequest("Time invalid");
            if (serviceRepository.GetByID(svc.ServiceId).ServiceSchedulers.Any(r => (((r.StartDate <= serviceSchedulerDTO.StartDate && serviceSchedulerDTO.StartDate <= r.EndDate)
                || (r.StartDate <= serviceSchedulerDTO.EndDate && serviceSchedulerDTO.EndDate <= r.EndDate)
                || (serviceSchedulerDTO.StartDate <= r.StartDate && serviceSchedulerDTO.EndDate >= r.EndDate)) && r.ItemId != serviceSchedulerDTO.ItemId))) return BadRequest("Scheduler is conflict time with orther in Service");
            try
            {
                serviceSchedulerRepository.UpdateServiceScheduler(mapper.Map<ServiceScheduler>(serviceSchedulerDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Updated");
        }

        [Authorize]
        [HttpDelete("[controller]")]
        public ActionResult DeleteScheduler(string Id)
        {
            ServiceScheduler svc = serviceSchedulerRepository.GetAll().SingleOrDefault(p => p.ItemId == Id);
            if (svc == null) return BadRequest("Not found service scheduler to delete");
            if (svc.Status == false || svc.Service.Status == false) return BadRequest("Not found scheduler to delete");
            try
            {
                serviceSchedulerRepository.RemoveServiceScheduler(svc.ServiceId, svc.StartDate);
                if (serviceRepository.GetByID(svc.ServiceId).ServiceSchedulers.Count() == 1) {
                    serviceRepository.RemoveService(svc.ServiceId);
                    postRepository.RemovePost(serviceRepository.GetByID(svc.ServiceId).PostId);
                } 
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deteted");
        }
    }
}
