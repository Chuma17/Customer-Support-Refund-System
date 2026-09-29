namespace CustomerSupportRefundSystem_API.Models;

public class RefundResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    public string Decision { get; set; } = string.Empty;
    public string DecisionReason { get; set; } = string.Empty;

    public string? AiResponse { get; set; }
    public DateTime CreatedAt { get; set; }
}
