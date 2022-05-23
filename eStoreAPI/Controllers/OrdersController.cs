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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string searchValue, string orderBy, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return BadRequest();
                }

                var orders = _orderRepository.GetOrders(searchValue, pageIndex, pageSize, orderBy);
                return Ok(orders.Select(e => _mapper.Map<Order, OrderDto>(e)));
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
                var order = _orderRepository.GetOrderById(id);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var entity = _mapper.Map<Order>(dto);

                _orderRepository.SaveOrder(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.OrderId }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
