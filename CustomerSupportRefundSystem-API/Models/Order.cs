namespace CustomerSupportRefundSystem_API.Models;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public string ProductName { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime OrderDate { get; set; }

    public bool IsFinalSale { get; set; }
    public bool IsDamaged { get; set; }
    public bool IsIncorrectItem { get; set; }
}
