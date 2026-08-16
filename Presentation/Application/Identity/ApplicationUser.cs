using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Application.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; }
    public string Roles { get; set; } = string.Empty;
}