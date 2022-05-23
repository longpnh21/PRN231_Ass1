using AutoMapper;
using BusinessObject.Dtos;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]/{orderId}")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId, [FromQuery] string searchValue, string orderBy, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1 || orderId < 1)
                {
                    return BadRequest();
                }

                var orders = _orderDetailRepository.GetOrderDetails(orderId, searchValue, pageIndex, pageSize, orderBy)
                    .Select(e => _mapper.Map<OrderDetailDto>(e));
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{productId}")]
        public IActionResult GetById(int orderId, int productId)
        {

            try
            {
                var order = _orderDetailRepository.GetOrderDetailById(orderId, productId);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<OrderDetailDto>(order));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
