using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Order;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderServices _order;

    public OrderController(OrderServices order)
    {
        _order = order;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orderList = await _order.GetOrdersAsync();

        return Ok(new Response<List<DisplayOrder>>
        {
            Success = true,
            Message = "Order List fetched",
            Data = orderList
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddOrders([FromBody] CreateOrder createOrder)
    {
        await _order.AddOrders(createOrder);

        return Ok(new Response<CreateOrder>
        {
            Success = true,
            Message = "Order added",
            Data = createOrder
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrdersById(Guid orderId)
    {
        var order = await _order.GetOrderById(orderId);

        return Ok(new Response<DisplayOrder>
        {
            Success = true,
            Message = "Order fetched",
            Data = order
        });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeOrderStatus(Guid orderId, [FromBody] ChangeOrderStatus changeOrderStatus)
    {
        var orderStatusToBeUpdated = await _order.UpdateOrderStatus(orderId, changeOrderStatus);

        return Ok(new Response<ChangeOrderStatus>
        {
            Success = true,
            Message = "Order status updated",
            Data = orderStatusToBeUpdated
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveOrder(Guid orderId)
    {
        var orderToRemove = await _order.GetOrderById(orderId);
        await _order.RemoveOrder(orderId);

        return Ok(new Response<DisplayOrder>
        {
            Success = true,
            Message = "Order removed",
            Data = orderToRemove
        });
    }
}