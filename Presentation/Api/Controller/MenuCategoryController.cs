using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Menu.Category;
using OrderManagement.Application.Services;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class MenuCategoryController : ControllerBase
{
    private readonly MenuCategoryService _menu;

    public MenuCategoryController(MenuCategoryService menu)
    {
        _menu = menu;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetMenuCategoriesAsync()
    {
        var menuList = await _menu.GetMenuCategoriesAsync();

        return Ok(new Response<List<DisplayMenuCategory>>
        {
            Success = true,
            Message = "Menu List fetched",
            Data = menuList
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuCategoryById(Guid categoryID)
    {
        var menuById = await _menu.GetMenuCategoryById(categoryID);

        return Ok(new Response<DisplayMenuCategory>
        {
            Success = true,
            Message = "Menu with ID fetched",
            Data = menuById
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMenuCategory([FromBody] CreateMenuCategory createMenuCategory)
    {
        await _menu.AddMenuCategory(createMenuCategory);

        return Ok(new Response<CreateMenuCategory>
        {
            Success = true,
            Message = "Menu Category added",
            Data = createMenuCategory
        });
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditMenuCategory([FromBody] EditMenuCategory editMenuCategory, Guid categoryID)
    {
        await _menu.EditCategory(categoryID, editMenuCategory);

        return Ok(new Response<EditMenuCategory>
        {
            Success = true,
            Message = "Menu Category Edited",
            Data = editMenuCategory
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveMenuCategory(Guid menuID)
    {
        await _menu.RemoveCategory(menuID);

        return Ok(new Response<string>
        {
            Success = true,
            Message = "Menu Category Removed successfully",
            Data = menuID.ToString()
        });
    }
}