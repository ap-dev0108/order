using System.ComponentModel.DataAnnotations;
using OrderManagement.Domain.Enum;

namespace OrderManagement.Domain.Entities;

public class Payment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid DinningSessionId { get; set; }

    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaidAt { get; set; }
}