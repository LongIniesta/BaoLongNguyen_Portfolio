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
    public class ProductsController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IAccountRepository accountRepository;
        private IProductRepository productRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        private IPostRepository postRepository;
        public ProductsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();
            accountRepository = new AccountRepository();
            productRepository = new ProductRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
            postRepository = new PostRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<ProductDTO>> GetProducts()
        {
            List<ProductDTO> result;
            try
            {
                result = mapper.Map<List<ProductDTO>>(productRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize]
        [HttpPut("[controller]")]
        public ActionResult UpdateProduct([FromBody] ProductDTO productDTO)
        {
            Product product = productRepository.GetAll().SingleOrDefault(p => p.ProductId == productDTO.ProductId);
            if (product == null) return BadRequest("Not found product to update");
            if (product.Status == false || product.Post.Status == false) return BadRequest("Not found product to update");
            try
            {
                productRepository.UpdateProduct(mapper.Map<Product>(productDTO));
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
            Product product = productRepository.GetAll().SingleOrDefault(p => p.ProductId == Id);
            if (product == null) return BadRequest("Not found product to delete");
            if (product.Status == false || product.Post.Status == false) return BadRequest("Not found product to delete");
            try
            {
                productRepository.RemoveProduct(Id);
                if (postRepository.GetByID(product.PostId).Products.Count() == 1) postRepository.RemovePost(product.PostId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deteted");
        }
    }
}
