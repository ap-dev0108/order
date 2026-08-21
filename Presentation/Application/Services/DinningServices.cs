using OrderManagement.Application.DTO.Dinning;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class DinningServices
{
    private readonly IDinningRepo _dinning;
    private readonly IDataRepo _data;

    public DinningServices(IDinningRepo dinningRepo, IDataRepo data)
    {
        _dinning = dinningRepo;
        _data = data;
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
            EndedAt = dinning.EndedAt
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
            EndedAt = dinning.EndedAt
        };
    }

    public async Task AddDinning(AddDinnerDTO addDinnerDTO)
    {
        var dinningSessionToAdd = new DinningSession
        {
            Table = addDinnerDTO.Table,
            TableId = addDinnerDTO.TableId,
            Status = addDinnerDTO.Status,
            StartedAt = addDinnerDTO.StartedAt,
            EndedAt = addDinnerDTO.EndedAt
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
        DinnerSessionToEdit.EndedAt = editDinnerDTO.EndedAt;

        await _data.SaveChangesAsync();
    }
}