using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Orders;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly OrderItemServices _orderItem;

    public OrderItemController(OrderItemServices orderItem)
    {
        _orderItem = orderItem;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetOrderItemsAsync()
    {
        var itemsList = await _orderItem.GetOrderItemsAsync();

        return Ok(new Response<List<DisplayOrderItem>>
        {
            Success = true,
            Message = "Items List fetched",
            Data = itemsList
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItemsById(Guid itemsId)
    {
        var item = await _orderItem.GetOrderItemsById(itemsId);

        return Ok(new Response<DisplayOrderItem>
        {
            Success = true,
            Message = "Items fetched",
            Data = item
        });
    }

    [HttpGet("{id}/order")]
    public async Task<IActionResult> GetOrderItemsByOrders(Guid orderId)
    {
        var orderItems = await _orderItem.GetOrderItemByOrder(orderId);

        return Ok(new Response<DisplayOrderItem>
        {
            Success = true,
            Message = "Order Items fetched",
            Data = orderItems
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddOrderItem(Guid orderId, CreateOrderItem createOrderItem)
    {
        await _orderItem.AddOrderItem(orderId, createOrderItem);

        return Ok(new Response<CreateOrderItem>
        {
            Success = true,
            Message = "Order Item has been added",
            Data = createOrderItem
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveOrderItem(Guid itemId)
    {
        await _orderItem.RemoveOrderItem(itemId);

        return Ok(new Response<Guid>
        {
            Success = true,
            Message = "Order Item has been removed",
            Data = itemId
        });
    }
}