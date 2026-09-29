namespace CustomerSupportRefundSystem_API.Services;

public interface IAiService
{
    Task<string> GenerateResponseAsync(
        string customerMessage,
        string customerName,
        string productName,
        decimal amount,
        string decision,
        string decisionReason);
}
