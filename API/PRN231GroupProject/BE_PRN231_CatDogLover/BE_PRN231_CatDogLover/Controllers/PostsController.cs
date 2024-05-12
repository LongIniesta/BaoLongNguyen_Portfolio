using AutoMapper;
using BusinessObjects;
using DTOs;
using DTOs.Account;
using DTOs.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interface;

namespace BE_PRN231_CatDogLover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IPostRepository postRepository;
        private IProductRepository productRepository;
        private IGiftRepository giftRepository;
        private IServiceRepository serviceRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        public PostsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            postRepository = new PostRepository();
            productRepository = new ProductRepository();
            giftRepository = new GiftRepository();
            serviceRepository = new ServiceRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpLoadPost([FromBody] PostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Data invalid");
            if (postDTO.Type.ToLower() == "product")
            {
                if (postDTO.Products == null || postDTO.Products.Count <= 0) return BadRequest("Not found any product");
                try
                {
                    Post post = mapper.Map<Post>(postDTO);
                    post.CreateDate = DateTime.Now;
                    post.Status = true;
                    post.Type = "product";
                    int postId = postRepository.AddPost(post).PostId;
                    foreach (ProductDTO productDTO in postDTO.Products)
                    {
                        productDTO.PostId = postId;
                        productDTO.Status = true;
                        productRepository.AddProduct(mapper.Map<Product>(productDTO));
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Created("", postDTO);
            }
            else if (postDTO.Type.ToLower() == "gift")
            {
                if (postDTO.Gifts == null || postDTO.Gifts.Count <= 0) return BadRequest("Not found any gift");
                try
                {

                    Post post = mapper.Map<Post>(postDTO);
                    post.CreateDate = DateTime.Now;
                    post.Status = true;
                    post.Type = "gift";
                    int postId = postRepository.AddPost(post).PostId;
                    foreach (GiftDTO giftDTO in postDTO.Gifts)
                    {
                        giftDTO.PostId = postId;
                        giftDTO.Status = true;
                        giftRepository.AddGift(mapper.Map<Gift>(giftDTO));
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Created("", postDTO);
            }
            else if (postDTO.Type.ToLower() == "service")
            {
                if (postDTO.Services == null || postDTO.Services.Count <= 0) return BadRequest("Not found any gift");
                foreach (ServiceDTO serviceDTO in postDTO.Services)
                {
                    if (serviceDTO.ServiceSchedulers == null || serviceDTO.ServiceSchedulers.Count <= 0) return BadRequest("Not found any scheduler in " + serviceDTO.ServiceName);
                    if (!checkScheduler(serviceDTO.ServiceSchedulers.ToList())) return BadRequest("Service scheduler of " + serviceDTO.ServiceName + " is invalid!");
                }
                try
                {
                    Post post = mapper.Map<Post>(postDTO);
                    post.CreateDate = DateTime.Now;
                    post.Status = true;
                    post.Type = "service";
                    int postId = postRepository.AddPost(post).PostId;
                    foreach (ServiceDTO serviceDTO in postDTO.Services)
                    {
                        serviceDTO.PostId = postId;
                        serviceDTO.Status = true;
                        string serviceId = serviceRepository.AddService(mapper.Map<Service>(serviceDTO)).ServiceId;
                        foreach (ServiceSchedulerDTO serviceSchedulerDTO in serviceDTO.ServiceSchedulers)
                        {
                            serviceSchedulerDTO.ServiceId = serviceId;
                            serviceSchedulerDTO.Status = true;
                            serviceSchedulerRepository.AddServiceScheduler(mapper.Map<ServiceScheduler>(serviceSchedulerDTO));
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Created("", postDTO);
            }
            else { return BadRequest("Post'type invalid!"); }
        }


        private bool checkScheduler(List<ServiceSchedulerDTO> list) {
            for (int i = 0; i < list.Count; i++) {
                if (list.Count(r => ((r.StartDate <= list[i].StartDate && list[i].StartDate <= r.EndDate)
                || (r.StartDate <= list[i].EndDate && list[i].EndDate <= r.EndDate)
                || (list[i].StartDate <= r.StartDate && list[i].EndDate >= r.EndDate))) > 1) {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Get the list of Posts include other tables (Gifts, Products, Services)
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("detail")]
        public IActionResult SearchDetailList(PostSearchRequest searchRequest)
        {
            try
            {
                var notMappedResponse = postRepository.Search(searchRequest);
                var response = mapper.Map<List<PostDTO>>(notMappedResponse.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get the list of Posts NOT include any other tables
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("general")]
        public  IActionResult SearchGeneralList(PostSearchRequest searchRequest)
        {
            try
            {
                var notMappedResponse = postRepository.Search(searchRequest);
                var response = mapper.Map<List<PostGeneralInformationResponse>>(notMappedResponse.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var notMappedResponse = await postRepository.GetPostByIdAsync(id);
                var response = mapper.Map<PostDTO>(notMappedResponse);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getStatitic")]
        public async Task<ActionResult<PostStatitic>> GetStatitic()
        {
            try
            {
                PostStatitic postStatitic = new PostStatitic { 
                    postToday = postRepository.GetAll().Count(p => p.CreateDate > DateTime.Today),
                    postTotal = postRepository.GetAll().Count()
                };
                return Ok(postStatitic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
