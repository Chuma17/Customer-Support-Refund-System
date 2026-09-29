namespace CustomerSupportRefundSystem_API.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<RefundRequest> RefundRequests { get; set; } = new List<RefundRequest>();
}
