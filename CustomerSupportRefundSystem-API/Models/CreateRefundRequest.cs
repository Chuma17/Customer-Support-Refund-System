namespace CustomerSupportRefundSystem_API.Models;

public class CreateRefundRequest
{
    public int OrderId { get; set; }
    public string CustomerMessage { get; set; } = string.Empty;
}
