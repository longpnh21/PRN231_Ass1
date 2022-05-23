using AutoMapper;
using BusinessObject;
using BusinessObject.Dtos;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;


namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string searchValue, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest();
                }

                var categories = _categoryRepository.GetCategories(searchValue, pageIndex, pageSize).Select(e => _mapper.Map<CategoryDto>(e));
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var category = _categoryRepository.FindCategoryById(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<Category>(dto);

                _categoryRepository.SaveCategory(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.CategoryId }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var inDatabase = _categoryRepository.FindCategoryById(id);
                if (inDatabase == null)
                {
                    return NotFound();
                }

                var entity = _mapper.Map<Category>(dto);
                entity.CategoryId = id;

                _categoryRepository.UpdateCategory(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _categoryRepository.FindCategoryById(id);
                if (category is null)
                {
                    return NotFound();
                }
                _categoryRepository.DeleteCategory(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
