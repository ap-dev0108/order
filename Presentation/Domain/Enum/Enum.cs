namespace OrderManagement.Domain.Enum;

public enum DinningStatus
{
    Active = 0,
    Close = 1
}

public enum Unit
{
    Grams = 0,
    Milliliters = 1,
    Pieces = 2
}

public enum OrderStatus
{
    Received = 0,
    Preparing = 1,
    Ready = 2,
    Served = 3,
    Cancelled = 4,
    Completed = 5
}

public enum PaymentMethod
{
    Cash = 0,
    Card = 1,
    Online = 2
}

public enum PaymentStatus
{
    Pending=0,
    Completed=1,
    Refunded=2
}