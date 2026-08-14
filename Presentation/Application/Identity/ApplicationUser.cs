using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Application.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName {get; set;} = string.Empty;
    public TimestampAttribute CreatedAt {get; set;}
    public bool IsActive {get; set;}
}