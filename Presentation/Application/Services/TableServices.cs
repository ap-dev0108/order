using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderManagement.Application.DTO.Table;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class TableServices
{
    private readonly ITableRepo _tableRepo;
    private readonly IDataRepo _data;

    public TableServices(ITableRepo tableRepo, IDataRepo data)
    {
        _tableRepo = tableRepo;
        _data = data;
    }

    public async Task<List<TableDTO>> GetRestaurantTablesAsync()
    {
        var tableList = await _tableRepo.GetRestaurantTablesAsync() ??
            throw new KeyNotFoundException("Table list not found");

        return tableList.Select(s => new TableDTO
        {
            Id = s.Id,
            TableNumber = s.TableNumber,
            QrCodeToken = s.QrCodeToken,
            Capacity = s.Capacity,
            IsActive = s.IsActive
        }).ToList();
    }
    public async Task<TableDTO> GetRestaurantTablesById(Guid id)
    {
        var tableById = await _tableRepo.GetRestaurantTableByIdAsync(id) ??
            throw new KeyNotFoundException($"Table with ID:{id} cannot be found");

        return new TableDTO
        {
            TableNumber = tableById.TableNumber,
            QrCodeToken = tableById.QrCodeToken,
            Capacity = tableById.Capacity,
            IsActive = tableById.IsActive
        };
    }
    public async Task AddRestaurantTables(AddTableDTO addTableDTO)
    {
        var tableToAdd = new RestaurantTable
        {
            TableNumber = addTableDTO.TableNumber,
            QrCodeToken = addTableDTO.QrCodeToken,
            Capacity = addTableDTO.Capacity,
            IsActive = addTableDTO.IsActive
        };

        await _tableRepo.AddRestaurantTable(tableToAdd);
        await _data.SaveChangesAsync();
    }
    public async Task RemoveTables(Guid id)
    {
        var tableToRemove = await _tableRepo.GetRestaurantTableByIdAsync(id) ??
            throw new KeyNotFoundException($"Table with the ID: {id} cannot be found");

        await _tableRepo.RemoveTable(tableToRemove);
        await _data.SaveChangesAsync();
    }
}