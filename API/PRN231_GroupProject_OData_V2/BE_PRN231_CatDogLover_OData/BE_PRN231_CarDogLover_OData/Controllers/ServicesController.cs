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
    public class ServicesController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceRepository serviceRepository;
        private IPostRepository postRepository;
        public ServicesController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();
            accountRepository = new AccountRepository();
            productRepository = new ProductRepository();
            serviceRepository = new ServiceRepository();
            postRepository = new PostRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<ServiceDTO>> GetServices()
        {
            List<ServiceDTO> result;
            try
            {
                result = mapper.Map<List<ServiceDTO>>(serviceRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize]
        [HttpPut("[controller]")]
        public ActionResult UpdateService([FromBody] ServiceDTO serviceDTO)
        {
            Service sv = serviceRepository.GetAll().SingleOrDefault(p => p.ServiceId == serviceDTO.ServiceId && serviceDTO.PostId == p.PostId);
            if (sv == null) return BadRequest("Not found service to update");
            if (sv.Status == false || sv.Post.Status == false) return BadRequest("Not found service to update");
            serviceDTO.Status = true;
            serviceDTO.ServiceSchedulers = null;
            try
            {
                serviceRepository.UpdateService(mapper.Map<Service>(serviceDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Updated");
        }

        [Authorize]
        [HttpDelete("[controller]")] 
        public ActionResult DeleteProduct(string Id)
        {
            Service sv = serviceRepository.GetAll().SingleOrDefault(p => p.ServiceId == Id);
            if (sv == null) return BadRequest("Not found service to delete");
            if (sv .Status == false || sv.Post.Status == false) return BadRequest("Not found service to delete");
            try
            {
                serviceRepository.RemoveService(Id);
                postRepository.RemovePost(sv.PostId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deteted");
        }
    }
}
