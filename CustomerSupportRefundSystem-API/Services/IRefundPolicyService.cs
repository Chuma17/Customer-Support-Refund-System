using CustomerSupportRefundSystem_API.Models;

namespace CustomerSupportRefundSystem_API.Services;

public interface IRefundPolicyService
{
    PolicyResult Evaluate(Order order, string customerMessage);
}
