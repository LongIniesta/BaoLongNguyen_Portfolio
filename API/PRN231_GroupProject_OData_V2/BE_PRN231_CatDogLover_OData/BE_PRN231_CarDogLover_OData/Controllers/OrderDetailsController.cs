using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Interface;
using Repositories;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class OrderDetailsController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        public OrderDetailsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();
            accountRepository = new AccountRepository();
            productRepository = new ProductRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<OrderDetailDTO>> GetOrderDetails()
        {
            List<OrderDetailDTO> result;
            try
            {
                result = mapper.Map<List<OrderDetailDTO>>(orderDetailRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }
    }
}
