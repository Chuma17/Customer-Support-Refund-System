using CustomerSupportRefundSystem_API.Data;
using CustomerSupportRefundSystem_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportRefundSystem_API.Services;

public class RefundService : IRefundService
{
    private readonly AppDbContext _db;
    private readonly IRefundPolicyService _policyService;
    private readonly IAiService _aiService;

    public RefundService(
        AppDbContext db,
        IRefundPolicyService policyService,
        IAiService aiService)
    {
        _db = db;
        _policyService = policyService;
        _aiService = aiService;
    }

    public async Task<RefundResponse> CreateAsync(
        CreateRefundRequest request)
    {
        var order = await _db.Orders
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == request.OrderId);

        if (order is null)
            throw new KeyNotFoundException(
                $"Order {request.OrderId} was not found.");

        var policyResult = _policyService.Evaluate(
            order,
            request.CustomerMessage);

        var aiResponse = await _aiService.GenerateResponseAsync(
            request.CustomerMessage,
            order.Customer.Name,
            order.ProductName,
            order.Amount,
            policyResult.Decision.ToString(),
            policyResult.Reason);

        var refundRequest = new RefundRequest
        {
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            CustomerMessage = request.CustomerMessage,
            Decision = policyResult.Decision.ToString(),
            DecisionReason = policyResult.Reason,
            AiResponse = aiResponse,
            CreatedAt = DateTime.UtcNow
        };

        _db.RefundRequests.Add(refundRequest);

        await _db.SaveChangesAsync();

        return new RefundResponse
        {
            Id = refundRequest.Id,
            OrderId = order.Id,
            CustomerName = order.Customer.Name,
            ProductName = order.ProductName,
            Amount = order.Amount,
            Decision = refundRequest.Decision,
            DecisionReason = refundRequest.DecisionReason,
            AiResponse = aiResponse,
            CreatedAt = refundRequest.CreatedAt
        };
    }

    public async Task<List<RefundResponse>> GetAllAsync()
    {
        return await _db.RefundRequests
            .Include(x => x.Customer)
            .Include(x => x.Order)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new RefundResponse
            {
                Id = x.Id,
                OrderId = x.OrderId,
                CustomerName = x.Customer.Name,
                ProductName = x.Order.ProductName,
                Amount = x.Order.Amount,
                Decision = x.Decision,
                DecisionReason = x.DecisionReason,
                AiResponse = x.AiResponse,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }
}