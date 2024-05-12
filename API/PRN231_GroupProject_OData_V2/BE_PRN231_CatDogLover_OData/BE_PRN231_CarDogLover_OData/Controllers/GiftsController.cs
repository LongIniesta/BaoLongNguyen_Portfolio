using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories;
using Repositories.Interface;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class GiftsController : ControllerBase
    {

        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IGiftCommentRepository giftCommentRepository;
        private IGiftRepository giftRepository;
        private IAccountRepository accountRepository;
        private IPostRepository postRepository;
        public GiftsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            giftCommentRepository = new GiftCommentRepository();
            giftRepository = new GiftRepository();
            accountRepository = new AccountRepository();
            postRepository = new PostRepository();
        }
        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<GiftDTO>> GetGifts()
        {
            List<GiftDTO> result;
            try
            {
                result = mapper.Map<List<GiftDTO>>(giftRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }
    }
}
