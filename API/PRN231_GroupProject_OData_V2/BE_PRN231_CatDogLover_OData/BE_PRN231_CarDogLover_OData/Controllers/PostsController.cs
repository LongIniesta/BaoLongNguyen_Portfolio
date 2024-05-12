using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.Interface;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class PostsController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IPostRepository postRepository;
        private IProductRepository productRepository;
        private IGiftRepository giftRepository;
        private IServiceRepository serviceRepository;
        private IServiceSchedulerRepository serviceSchedulerRepository;
        private IReactRepository reactRepository;
        public PostsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            postRepository = new PostRepository();
            productRepository = new ProductRepository();
            giftRepository = new GiftRepository();
            serviceRepository = new ServiceRepository();
            serviceSchedulerRepository = new ServiceSchedulerRepository();
            reactRepository = new ReactRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<PostDTO>> GetPosts(int accountId)
        {
            List<PostDTO> result;
            try
            {
                if (accountId != 0) {
                    List<React> reacts = reactRepository.GetAll().Where(r => r.AccountId == accountId).ToList();
                    result = mapper.Map<List<PostDTO>>(postRepository.GetAll().ToList());
                    result.Where(p => reacts.Any(r => r.PostId == p.PostId)).ToList().ForEach(p => p.Reacted = true);
                } else 
                result = mapper.Map<List<PostDTO>>(postRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }

        [Authorize(Roles = "user")]
        [HttpPost("[controller]/Post")]
        public IActionResult UpLoadPost([FromBody] PostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Data invalid");
            postDTO.NumberOfReact = 0;
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


        private bool checkScheduler(List<ServiceSchedulerDTO> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Count(r => ((r.StartDate <= list[i].StartDate && list[i].StartDate <= r.EndDate)
                || (r.StartDate <= list[i].EndDate && list[i].EndDate <= r.EndDate)
                || (list[i].StartDate <= r.StartDate && list[i].EndDate >= r.EndDate))) > 1)
                {
                    return false;
                }
            }
            return true;
        }

        [Authorize]
        [HttpPut("[controller]")]
        public IActionResult UpdatePost([FromBody] PostDTO postDTO) {
            if (postDTO.PostId == null) return BadRequest("Not found postId");
            Post post = postRepository.GetByID((int)postDTO.PostId);
            if (post == null) return BadRequest("Not found Post");
            try
            {
                post.Content = postDTO.Content;
                post.Title = postDTO.Title;
                postRepository.UpdatePost(post);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok("Updated");
        }

        [Authorize]
        [HttpPut("[controller]/Disable/{postId}")]
        public IActionResult DisablePost(int postId)
        {
            if (postId == null) return BadRequest("Not found postId");
            Post post = postRepository.GetByID(postId);
            if (post == null) return BadRequest("Not found Post");
            try
            {
                post.Status = false;
                postRepository.UpdatePost(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated");
        }

        [Authorize]
        [HttpPut("[controller]/Enable/{postId}")]
        public IActionResult EnablePost(int postId)
        {
            if (postId == null) return BadRequest("Not found postId");
            Post post = postRepository.GetByID(postId);
            if (post == null) return BadRequest("Not found Post");
            try
            {
                post.Status = true;
                postRepository.UpdatePost(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated");
        }


        [Authorize]
        [HttpDelete("[controller]/{PostId}")]
        public IActionResult DeletePost(int PostId)
        {
            Post post = postRepository.GetByID((int)PostId);
            if (post == null) return BadRequest("Not found Post");
            try
            {
                if (post.Type == "product")
            {
                foreach (Product product in post.Products)
                {
                    product.Status = false;
                    productRepository.UpdateProduct(product);
                }
            }
            else if (post.Type == "service")
            {
                foreach (Service service in post.Services)
                {
                    service.Status = false;
                    serviceRepository.UpdateService(service);
                    foreach (ServiceScheduler svc in serviceSchedulerRepository.GetAll().Where(s => s.ServiceId == service.ServiceId))
                    {
                        svc.Status = false;
                        serviceSchedulerRepository.UpdateServiceScheduler(svc);
                    }
                }
            } 
            else if (post.Type == "gift") {
                foreach (Gift gift in post.Gifts) { 
                    gift.Status = false;
                    giftRepository.UpdateGift(gift);
                }
            }
                post.Status = false;
                postRepository.UpdatePost(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }

    }
}
