namespace OrderManagement.Application.DTO.Table;

public class TableDTO
{
    public Guid Id {get; set;}

    public int TableNumber {get; set;}

    public string QrCodeToken {get; set;} = string.Empty;

    public int Capacity {get; set;}

    public bool IsActive {get; set;}
}