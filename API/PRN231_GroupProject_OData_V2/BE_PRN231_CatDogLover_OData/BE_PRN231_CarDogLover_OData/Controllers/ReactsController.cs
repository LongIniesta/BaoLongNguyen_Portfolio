using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Interface;
using Repositories;
using DTOs;
using Microsoft.AspNetCore.OData.Query;
using BusinessObjects;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class ReactsController : ODataController
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
        public ReactsController(IConfiguration configuration, IMapper mapper)
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
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<ReactDTO>> GetReacts()
        {
            List<ReactDTO> result;
            try
            {
                result = mapper.Map<List<ReactDTO>>(reactRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize(Roles = "user")]
        [HttpPost("[Controller]")]
        public ActionResult AddReact([FromBody] ReactDTO reactDTO)
        {
            if (accountRepository.GetById(reactDTO.AccountId) == null) return BadRequest("Not found Account");
            if (reactTypeRepository.GetByID(reactDTO.ReactTypeId) == null) return BadRequest("Not found react type");
            if (postRepository.GetByID(reactDTO.PostId) == null) return BadRequest("not found post");
            try
            {
                Post post = postRepository.GetByID(reactDTO.PostId);
                if (post.Reacts.Any(r => r.AccountId == reactDTO.AccountId))
                {
                    if (post.Reacts.Any(r => r.AccountId == reactDTO.AccountId && r.ReactTypeId == reactDTO.ReactTypeId))
                    {
                        reactRepository.RemoveReact(reactDTO.AccountId, reactDTO.PostId);
                        post = postRepository.GetByID(reactDTO.PostId);
                        post.NumberOfReact = post.NumberOfReact - 1;
                        postRepository.UpdatePost(post);
                        return Ok("Unreacted");
                    }
                    else
                    {
                        reactRepository.RemoveReact(reactDTO.AccountId, reactDTO.PostId);
                        reactRepository.AddReact(mapper.Map<React>(reactDTO));
                        return Ok("Change react");
                    }
                }
                else {
                    reactRepository.AddReact(mapper.Map<React>(reactDTO));
                    post.NumberOfReact = post.NumberOfReact + 1;
                    postRepository.UpdatePost(post);
                    return Ok("Reacted");
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
