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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string searchValue, string orderBy, int pageIndex = 1, int pageSize = 50)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest();
                }

                var products = _productRepository.GetProducts(searchValue, pageIndex, pageSize, orderBy);
                return Ok(products.Select(e => _mapper.Map<ProductDto>(e)));
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
                var product = _productRepository.GetProductById(id);
                return Ok(_mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<Product>(dto);

                _productRepository.SaveProduct(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.ProductId }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var inDatabase = _productRepository.GetProductById(id);

                if (inDatabase == null)
                {
                    return NotFound();
                }

                var entity = _mapper.Map<Product>(dto);
                entity.ProductId = id;

                _productRepository.UpdateProduct(entity);
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
                var product = _productRepository.GetProductById(id);
                if (product is null)
                {
                    return NotFound();
                }
                _productRepository.DeleteProduct(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
