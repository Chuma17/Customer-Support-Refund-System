using CustomerSupportRefundSystem_API.Models;

namespace CustomerSupportRefundSystem_API.Services;

public interface IRefundService
{
    Task<RefundResponse> CreateAsync(CreateRefundRequest request);
    Task<List<RefundResponse>> GetAllAsync();
}
