using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Menu.Category;
using OrderManagement.Application.Interface;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class MenuCategoryController : ControllerBase
{
    private readonly MenuCategoryService _menu;
    private readonly ISearchable _searchable;

    public MenuCategoryController(MenuCategoryService menu, ISearchable searchable)
    {
        _menu = menu;
        _searchable = searchable;
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

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var search = await _searchable.SearchByNameAsync<MenuCategory>(m => m.MenuCategoryTitle == term);

        return Ok(new Response<List<MenuCategory>>
        {
            Success = true,
            Message = "Search api executed",
            Data = search
        });
    }
}