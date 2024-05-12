using AutoMapper;
using BusinessObjects;
using DataAccess;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Query;
using Repositories;
using Repositories.Interface;

namespace BE_PRN231_CarDogLover_OData.Controllers
{
    public class OrdersController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        public OrdersController(IConfiguration configuration, IMapper mapper)
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
        public ActionResult<List<OrderDTO>> GetOrders()
        {
            List<OrderDTO> result;
            try
            {
                result = mapper.Map<List<OrderDTO>>(orderRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize(Roles = "user")]
        [HttpPost("[controller]/Add")]
        public ActionResult<CategoryDTO> AddOrder([FromBody] OrderDTO orderDTO)
        {
            if (!accountRepository.GetAll().Any(a => a.AccountId == orderDTO.AccountId && a.Status == true)) return BadRequest("Not found seller account to order");
            if (orderDTO.OrderDetails == null || orderDTO.OrderDetails.Count == 0) return BadRequest("Order details is empty");
            if (orderDTO.OrderDetails.Count != orderDTO.OrderDetails.Select(o => o.ItemId).Distinct().Count()) return BadRequest("Item duplicated!");
            foreach (OrderDetailDTO orderDetailDTO in orderDTO.OrderDetails) {
                if (orderDetailDTO.Type == "product")
                {
                    Product product = productRepository.GetByItemID(orderDetailDTO.ItemId);
                    if (product == null || product.Status == false) return BadRequest("Not found item with id = " + orderDetailDTO.ItemId);
                    if (product.Post.OwnerId == orderDTO.AccountId) return BadRequest("Not order product of yourself");
                    if (product.Price != orderDetailDTO.Price) return BadRequest("Price invalid item with id = " + orderDetailDTO.ItemId);
                    if (orderDetailDTO.ShipAddress == null || orderDetailDTO.ShipAddress.Trim() == "") BadRequest("Please fill shipaddress of item with id = " + orderDetailDTO.ItemId);
                }
                else
                if (orderDetailDTO.Type == "scheduler")
                {
                    ServiceScheduler svc = serviceSchedulerRepository.GetByItemID(orderDetailDTO.ItemId);

                    if (svc == null || svc.Status == false || svc.StartDate < DateTime.Now) return BadRequest("Not found item with id = " + orderDetailDTO.ItemId);
                    if (svc.Service.Post.OwnerId == orderDTO.AccountId) return BadRequest("Not order service of yourself");
                    if (svc.Price != orderDetailDTO.Price) return BadRequest("Price invalid item with id = " + orderDetailDTO.ItemId);
                }
                else {
                    return BadRequest("Type of item id = " + orderDetailDTO.ItemId + " invalid!");
                }
            }
            try
            {
                orderDTO.TotalPrice = orderDTO.OrderDetails.Sum(od => od.Price);
                orderDTO.Status = true;
                orderDTO.OrderDate = DateTime.Now;
                int OrderId = orderRepository.AddOrder(mapper.Map<Order>(orderDTO)).OrderId;
                foreach (OrderDetailDTO orderDetailDTO1 in orderDTO.OrderDetails)
                {
                    orderDetailDTO1.OrderId = OrderId;
                    orderDetailRepository.AddOrderDetail(mapper.Map<OrderDetail>(orderDetailDTO1));
                    if (orderDetailDTO1.Type == "product")
                    {
                        Product product = productRepository.GetByItemID(orderDetailDTO1.ItemId);
                        product.Status = false;
                        productRepository.UpdateProduct(product);
                    }
                    else
                if (orderDetailDTO1.Type == "scheduler")
                    {
                        ServiceScheduler svc = serviceSchedulerRepository.GetByItemID(orderDetailDTO1.ItemId);
                        svc.Status = false;
                        serviceSchedulerRepository.UpdateServiceScheduler(svc);
                    }
                }
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            return Created("", orderDTO);
        }
    }
}
