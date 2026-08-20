namespace OrderManagement.Application.DTO;

public class UserDTO
{
    public string Id {get; set;}
    public string? FullName {get;set;}
    public string? Email {get; set;}
    public bool? isActive {get; set;}
}

public class EditUserDTO
{
    public string? FullName {get; set;}
    public string? Email {get; set;}
    public string? Username {get; set;} 
}