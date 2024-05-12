using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Interface;
using Repositories;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class GiftCommentsController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private IGiftCommentRepository giftCommentRepository;
        private IGiftRepository giftRepository;
        private IAccountRepository accountRepository;
        private IPostRepository postRepository;
        public GiftCommentsController(IConfiguration configuration, IMapper mapper)
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
        public ActionResult<List<GiftCommentDTO>> GetGiftComments()
        {
            List<GiftCommentDTO> result;
            try
            {
                result = mapper.Map<List<GiftCommentDTO>>(giftCommentRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return result;
        }


        [Authorize(Roles = "user")]
        [HttpPost("[controller]")]
        public IActionResult PostGiftComment([FromBody] GiftCommentDTO giftCommentDTO)
        {
            GiftCommentDTO result;
            try
            {
                if (!giftRepository.GetAll().Any(g => g.GiftId == giftCommentDTO.GiftId && g.Status == true)) 
                    return BadRequest("Not found gift!");
                if (!accountRepository.GetAll().Any(a => a.Status == true && a.AccountId == giftCommentDTO.AccountId && a.RoleId == 1)) return BadRequest("Not found user");
                if (giftCommentRepository.GetAll().Any(g => g.AccountId == giftCommentDTO.AccountId && g.GiftId == giftCommentDTO.GiftId)) return BadRequest("User comment already");
                if (giftRepository.GetByID(giftCommentDTO.GiftId).Post.OwnerId == giftCommentDTO.AccountId) return BadRequest("Not comment in your post");
                giftCommentDTO.ApproveStatus = "waiting";
                giftCommentDTO.CreateDate = DateTime.Now;
                result = mapper.Map<GiftCommentDTO>(giftCommentRepository.AddGiftComment(mapper.Map<GiftComment>(giftCommentDTO)));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Created("", result);
        }
        [Authorize]
        [HttpDelete("[controller]/{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                GiftComment comment = giftCommentRepository.GetAll().SingleOrDefault(g => g.GiftCommentId == id);
                if (comment == null) return BadRequest("Not found comment");
                if (comment.ApproveStatus.ToLower().Equals("accept")) return BadRequest("Comment is accepted, can't delete");
                giftCommentRepository.RemoveGiftComment(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deleted");
        }

        [Authorize(Roles = "user")]
        [HttpPut("[controller]/{id}")]
        public IActionResult UpdateComment(int id, string content)
        {
            try
            {
                GiftComment comment = giftCommentRepository.GetAll().SingleOrDefault(g => g.GiftCommentId == id);
                if (comment == null) return BadRequest("Not found comment");
                if (!comment.ApproveStatus.ToLower().Equals("waiting") ) return BadRequest("Can't edit, comment is expried");
                comment.Content = content;
                giftCommentRepository.UpdateGiftComment(comment);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Updated");
        }

        [Authorize(Roles = "user")]
        [HttpPut("[controller]/Accept/{id}")]
        public IActionResult AcceptComment(int id)
        {
            try
            {
                GiftComment comment = giftCommentRepository.GetAll().SingleOrDefault(g => g.GiftCommentId == id);
                if (comment == null) return BadRequest("Not found comment");
                comment.ApproveStatus = "accept";
                giftCommentRepository.UpdateGiftComment(comment);
                List<GiftComment> list = giftCommentRepository.GetAll().Where(g => g.GiftId == comment.GiftId && g.GiftCommentId != id).ToList();
                foreach (GiftComment giftComment in list) {
                    giftComment.ApproveStatus = "denied";
                    giftCommentRepository.UpdateGiftComment(giftComment);
                }
                Gift gift = giftRepository.GetByID(comment.GiftId);
                gift.Status = false;
                giftRepository.UpdateGift(gift);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Accepted");
        }
    }
}
