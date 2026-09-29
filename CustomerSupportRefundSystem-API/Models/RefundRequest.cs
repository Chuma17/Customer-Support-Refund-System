namespace CustomerSupportRefundSystem_API.Models;

public class RefundRequest
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string CustomerMessage { get; set; } = null!;

    public string Decision { get; set; } = null!;
    public string DecisionReason { get; set; } = null!;

    public string? AiResponse { get; set; }

    public DateTime CreatedAt { get; set; }
}
