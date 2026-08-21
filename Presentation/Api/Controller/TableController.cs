using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Table;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly TableServices _tableServices;

    public TableController(TableServices tableServices)
    {
        _tableServices = tableServices;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTablesAsync()
    {
        var tableList = await _tableServices.GetRestaurantTablesAsync();

        return Ok(new Response<List<TableDTO>>
        {
            Success = true,
            Message = "Table List fetched",
            Data = tableList
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTableById(Guid id)
    {
        var table = await _tableServices.GetRestaurantTablesById(id);

        return Ok(new Response<TableDTO>
        {
            Success = true,
            Message = "Table fetched by ID",
            Data = table
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTables(AddTableDTO addTableDTO)
    {
        await _tableServices.AddRestaurantTables(addTableDTO);

        return Ok(new Response<AddTableDTO>
        {
            Success = true,
            Message = "Table added",
            Data = addTableDTO
        });
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditTables(Guid id, EditTableDTO editTableDTO)
    {
        await _tableServices.EditTableData(id, editTableDTO);

        return Ok(new Response<EditTableDTO>
        {
            Success = true,
            Message = "Table is edited",
            Data = editTableDTO
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveTables(Guid id)
    {
        var tableToRemove = await _tableServices.GetRestaurantTablesById(id);
        await _tableServices.RemoveTables(id);

        return Ok(new Response<TableDTO>
        {
            Success = true,
            Message = "Table with the provided ID has been removed",
            Data = tableToRemove
        });
    }
}