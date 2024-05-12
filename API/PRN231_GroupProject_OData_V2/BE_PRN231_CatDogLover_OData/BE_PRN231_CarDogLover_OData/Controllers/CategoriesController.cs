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
    public class CategoriesController : ODataController
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper mapper;
        private ICategoryRepository categoryRepository;
        public CategoriesController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            this.mapper = mapper;
            categoryRepository = new CategoryRepository();
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public ActionResult<List<CategoryDTO>> GetCategories()
        {
            List<Category> result;
            try
            {
                result = categoryRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return mapper.Map<List<CategoryDTO>>(result);
        }

        [Authorize(Policy = "AdminOrStaff")]
        [HttpPost("[controller]/Add")]
        public ActionResult<CategoryDTO> AddCategory([FromBody] CategoryDTO CategoryDTO)
        {
            Category result;
            if (categoryRepository.GetAll().Any(c => c.CategoryName.ToLower() == CategoryDTO.CategoryName.ToLower())) return new BadRequestObjectResult("Category is exits");
            try
            {
                result = categoryRepository.AddCategory(mapper.Map<Category>(CategoryDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Created("", result);

        }
        [Authorize(Policy = "AdminOrStaff")]
        [HttpDelete("[controller]/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (!categoryRepository.GetAll().Any(c => c.CategoryId == id)) return NotFound("Not found Category to delete");
            try
            {
                categoryRepository.RemoveCategory(id);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok("Deleted");
        }
        [Authorize(Policy = "AdminOrStaff")]
        [HttpPut("[controller]/Update")]
        public ActionResult Update([FromBody] CategoryDTO CategoryDTO)
        {
            Category result;
            if (!categoryRepository.GetAll().Any(c => c.CategoryId == CategoryDTO.CategoryId)) return NotFound("Not found Category to delete");
            try
            {
                result = categoryRepository.UpdateCategory(mapper.Map<Category>(CategoryDTO));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return Ok(mapper.Map<CategoryDTO>(result));
        }

    }
}
