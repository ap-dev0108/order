using System.ComponentModel.DataAnnotations;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Domain.Entities;

public class Ingredient
{
    [Key]
    public Guid Id {get; set;} = Guid.NewGuid();
    public string Title {get; set;} = string.Empty;
    public Unit Units {get; set;}
    public decimal QualityOnHand {get; set;}
    public decimal ReorderThreshold {get; set;}
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}