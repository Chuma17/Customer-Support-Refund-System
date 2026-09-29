using CustomerSupportRefundSystem_API.Models;

namespace CustomerSupportRefundSystem_API.Services;

public class RefundPolicyService : IRefundPolicyService
{
    private const int RefundWindowDays = 30;
    private const decimal ManualReviewThreshold = 500m;

    public PolicyResult Evaluate(Order order, string customerMessage)
    {
        if (order.IsFinalSale)
        {
            return new PolicyResult(
                RefundDecision.Denied,
                "This item was marked as final sale and is not eligible for a refund."
            );
        }

        if (order.OrderDate < DateTime.UtcNow.AddDays(-RefundWindowDays))
        {
            return new PolicyResult(
                RefundDecision.Denied,
                "This order is outside the 30-day refund window."
            );
        }

        if (order.Amount > ManualReviewThreshold)
        {
            return new PolicyResult(
                RefundDecision.Escalated,
                "Refunds above $500 require human review."
            );
        }

        if (order.IsDamaged || order.IsIncorrectItem)
        {
            var issue = order.IsDamaged
                ? "damaged"
                : "incorrect";

            return new PolicyResult(
                RefundDecision.Approved,
                $"The order qualifies because the item was reported as {issue}."
            );
        }

        return new PolicyResult(
            RefundDecision.Escalated,
            "The request does not match an automatic refund condition and requires human review."
        );
    }
}
