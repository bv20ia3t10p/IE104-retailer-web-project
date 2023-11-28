﻿using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _service;
        public OrderController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        [EnableQuery]

        public ActionResult<IEnumerable<OrderDto>> GetOrders() => Ok(_service.Order.GetOrders());
        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrder(int id) => Ok(_service.Order.GetOrder(id));
        [HttpGet("Customer/{customerId}")]
        [EnableQuery]
        public ActionResult<IEnumerable<OrderDto>> GetOrders(int customerId)
        {
            return Ok(_service.Order.GetOrdersByCustomer(customerId));
        }
        [HttpGet("Customer/Email/")]
        [Authorize(Roles ="USER,ADMINISTRATOR")]
        [EnableQuery]
        public ActionResult<IEnumerable<OrderDto>> GetOrdersByEmail([FromHeader] string Authorization)
        {
            var token = Authorization.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var email = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var customerInDb = _service.Customer.GetCustomerByEmail(email);
            if (customerInDb == null) return Unauthorized();
            return Ok(_service.Order.GetOrdersByCustomer(customerInDb.CustomerId));
        }
        [HttpPost]
        public ActionResult<OrderDto> CreateOrder(CreateOrderDto newOrder)
        {
            var createdOrder = _service.Order.CreateOrder(newOrder);
            foreach ( var od in newOrder.orderDetails )
            {
                od.OrderId = createdOrder.OrderId;
                _service.OrderDetail.CreateOrderDetail(od);
            }
            return CreatedAtAction(nameof(GetOrder),new {id = createdOrder.OrderId},_service.Order.GetOrder(createdOrder.OrderId));
        }
        [HttpPut("Status")]
        public ActionResult<OrderDto> UpdateOrderStatus(UpdateOrderStatusDto newOrderStatus)
        {
            var updatedOrder = _service.Order.UpdateOrderStatus(newOrderStatus);
            return Ok(updatedOrder);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            _service.Order.DeleteOrder(id);
            return NoContent();
        }

    }
}
