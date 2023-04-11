using AutoMapper;
using E_Book_Library.DTOs;
using E_Book_Library.Models;
using Ebook.Service.Services.Interfases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Book_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var response = _categoryService.GetCategories().ToList();
            var categories = _mapper.Map<List<Category>, List<CategoryDto>>(response);
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(categories == null)
            {
                return NotFound();
            }
            
            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);
        
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryDto);
            var result = _categoryService.CreateCategory(categoryMap);
            
            if (!result)
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500,ModelState);
            }

            return Ok("Succesfully Created");    
        }

        [HttpGet("categoryId")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            var categoryMap = _categoryService.GetCategory(categoryId);

            var response = _mapper.Map<CategoryDto>(categoryMap);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(response);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {  
            if(categoryDto == null)
                return BadRequest(ModelState);

            if(categoryId != categoryDto.Id)
            {
                return BadRequest();
            }

            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var mapCategory = _mapper.Map<Category>(categoryDto);
            var response = _categoryService.UpdateCategory(mapCategory);

            if (!response)
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Record updated");
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryToDelete = _categoryService.GetCategory(categoryId);
            
            if(categoryToDelete == null)
            {
                ModelState.AddModelError("","Not Found");
                return StatusCode(500, ModelState);
            }

            var result = _categoryService.DeleteCategory(categoryToDelete);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Record deleted Succesfully");
        }
    }
}
