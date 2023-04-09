using AutoMapper;
using E_Book_Library.DTOs;
using E_Book_Library.IServices;
using E_Book_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Book_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            CategoryResponseDto categoryResponseDto = new CategoryResponseDto();

            var categories = _mapper.Map<List<CategoryDto>>(_categoryService.GetCategories());
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(categories == null)
            {
                return NotFound();
            }
            else
            {
                categoryResponseDto.StatusCode = 200;
                categoryResponseDto.Message = "Users Found";
                categoryResponseDto.IsSuccess = true;
            }
            
            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            CategoryResponseDto categoryResponseDto = new CategoryResponseDto();

            if (categoryDto == null)
                return BadRequest(ModelState);

            var categories = _categoryService.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(categories != null)
            {
                categoryResponseDto.Message = "Category already exists";
                categoryResponseDto.StatusCode = 422;
                categoryResponseDto.IsSuccess = false;
                return Ok(categoryResponseDto);
                //ModelState.AddModelError("", categoryResponseDto.Message = "Category already exists");
                //return StatusCode(categoryResponseDto.StatusCode = 422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryDto);

            if (!_categoryService.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", categoryResponseDto.Message = "Something went wrong");
                return StatusCode(categoryResponseDto.StatusCode = 500,ModelState);
            }

            return Ok("Succesfully Created");
                
        }

        [HttpGet("categoryId")]
        public IActionResult GetCategory(int categoryId)
        {
            CategoryResponseDto categoryResponseDto = new CategoryResponseDto();

            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            var categoryMap = _categoryService.GetCategory(categoryId);

            var response = _mapper.Map<CategoryDto>(categoryMap);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(response);
        }
    }
}
