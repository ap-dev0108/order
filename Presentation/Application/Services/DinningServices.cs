using Microsoft.AspNetCore.Http;
using OrderManagement.Application.DTO.Dinning;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class DinningServices
{
    private readonly IDinningRepo _dinning;
    private readonly IDataRepo _data;
    private readonly ITableRepo _table;

    public DinningServices(IDinningRepo dinningRepo, IDataRepo data, ITableRepo table)
    {
        _dinning = dinningRepo;
        _data = data;
        _table = table;
    }

    public async Task<List<DinningDTO>> GetDinningDTOsAsync()
    {
        var dinningList = await _dinning.GetDinningSessionsAsync() ??
            throw new KeyNotFoundException("Dinning List not found");

        return dinningList.Select(dinning => new DinningDTO
        {
            Id = dinning.Id,
            Table = dinning.Table,
            TableId = dinning.TableId,
            Status = dinning.Status,
            StartedAt = dinning.StartedAt,
        }).ToList();
    }

    public async Task<DinningDTO> GetDinningById(Guid id)
    {
        var dinning = await _dinning.GetDinningSessionById(id) ??
            throw new KeyNotFoundException($"Dinning Session with the ID:{id} cannot be found");

        return new DinningDTO
        {
            Id = dinning.Id,
            Table = dinning.Table,
            TableId = dinning.TableId,
            Status = dinning.Status,
            StartedAt = dinning.StartedAt,
        };
    }

    public async Task AddDinning(Guid tableId, AddDinnerDTO addDinnerDTO)
    {
        var table = await _table.GetRestaurantTableByIdAsync(tableId) ??
            throw new KeyNotFoundException("Table with this ID not found");


        var session = await _dinning.HasActiveSession(tableId);
        if (session == true)
        {
            throw new BadHttpRequestException("Dinning Session is going on, cannot book the table");
        }

        var dinningSessionToAdd = new DinningSession
        {
            TableId = tableId,
            Table = table,
            Status = addDinnerDTO.Status,
            StartedAt = addDinnerDTO.StartedAt,
        };

        await _dinning.AddDinning(dinningSessionToAdd);
        await _data.SaveChangesAsync();
    }

    public async Task EditDinningSession(EditDinnerDTO editDinnerDTO, Guid DinningId)
    {
        var DinnerSessionToEdit = await _dinning.GetDinningSessionById(DinningId) ??
            throw new KeyNotFoundException($"Dinner Seesion with ID: {DinningId} cannot be found");

        DinnerSessionToEdit.Table = editDinnerDTO.Table;
        DinnerSessionToEdit.TableId = editDinnerDTO.TableId;
        DinnerSessionToEdit.Status = editDinnerDTO.Status;
        DinnerSessionToEdit.StartedAt = editDinnerDTO.StartedAt;

        await _data.SaveChangesAsync();
    }
}