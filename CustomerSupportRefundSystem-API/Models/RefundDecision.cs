namespace CustomerSupportRefundSystem_API.Models;

public enum RefundDecision
{
    Approved,
    Denied,
    Escalated
}

public record PolicyResult(
    RefundDecision Decision,
    string Reason
);
