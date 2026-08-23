using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Menu.Items;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

public class MenuItemController : ControllerBase
{
    private readonly MenuItemServices _menuServices;

    public MenuItemController(MenuItemServices menuServices)
    {
        _menuServices = menuServices;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetMenuItems()
    {
        var MenuList = await _menuServices.GetMenuItemsAsync();

        return Ok(new Response<List<DisplayMenuItem>>
        {
            Success = true,
            Message = "Menu Items List fetched",
            Data = MenuList
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenusById(Guid menuID)
    {
        var Menu = await _menuServices.GetMenuItemById(menuID);

        return Ok(new Response<DisplayMenuItem>
        {
            Success = true,
            Message = "Menu item by id fetched",
            Data = Menu
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMenus([FromBody] CreateMenuItem createMenuItem)
    {
        await _menuServices.AddMenuItem(createMenuItem);

        return Ok(new Response<CreateMenuItem>
        {
            Success = true,
            Message = "Menu created",
            Data = createMenuItem
        });
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditMenus([FromBody] EditMenuItem editMenuItem, Guid menuID)
    {
        await _menuServices.EditMenuItems(menuID, editMenuItem);

        return Ok(new Response<EditMenuItem>
        {
            Success = true,
            Message = "Menu Item edited",
            Data = editMenuItem
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveMenus(Guid menuID)
    {
        await _menuServices.RemoveMenuItem(menuID);

        return Ok(new Response<string>
        {
            Success = true,
            Message = "Menu Item removed",
            Data = menuID.ToString()
        });
    }
}