using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Dinning;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DinningSessionController : ControllerBase
{
    private readonly DinningServices _dinning;

    public DinningSessionController(DinningServices dinning)
    {
        _dinning = dinning;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetDinningSessions()
    {
        var dinningList = await _dinning.GetDinningDTOsAsync();

        return Ok(new Response<List<DinningDTO>>
        {
            Success = true,
            Message = "Dinning Sessions fetched",
            Data = dinningList
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDinningSessionsById(Guid id)
    {
        var dinning = await _dinning.GetDinningById(id);

        return Ok(new Response<DinningDTO>
        {
            Success = true,
            Message = "Dinning Fetched",
            Data = dinning
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddDinningSessions(AddDinnerDTO addDinnerDTO)
    {
        await _dinning.AddDinning(addDinnerDTO);

        return Ok(new Response<AddDinnerDTO>
        {
            Success = true,
            Message = "Dinner DTO fetched",
            Data = addDinnerDTO
        });
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditDinningSessions([FromBody] EditDinnerDTO editDinnerDTO, Guid id)
    {
        await _dinning.EditDinningSession(editDinnerDTO, id);

        return Ok(new Response<EditDinnerDTO>
        {
            Success = true,
            Message = "Dinner edited",
            Data = editDinnerDTO
        });
    }
}