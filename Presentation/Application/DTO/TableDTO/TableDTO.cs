using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.DTO.Table;

public class TableDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int TableNumber { get; set; }

    public string QrCodeToken { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public bool IsActive { get; set; }

    public ICollection<DinningSession> Sessions { get; set; }
}