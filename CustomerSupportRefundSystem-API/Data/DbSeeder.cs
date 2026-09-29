using CustomerSupportRefundSystem_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportRefundSystem_API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Customers.AnyAsync())
            return;

        var customers = new List<Customer>
        {
            new() { Name = "John Smith", Email = "john.smith@example.com" },
            new() { Name = "Sarah Johnson", Email = "sarah.johnson@example.com" },
            new() { Name = "Michael Brown", Email = "michael.brown@example.com" },
            new() { Name = "Emily Davis", Email = "emily.davis@example.com" },
            new() { Name = "Daniel Wilson", Email = "daniel.wilson@example.com" },
            new() { Name = "Jessica Miller", Email = "jessica.miller@example.com" },
            new() { Name = "David Anderson", Email = "david.anderson@example.com" },
            new() { Name = "Olivia Thomas", Email = "olivia.thomas@example.com" },
            new() { Name = "James Taylor", Email = "james.taylor@example.com" },
            new() { Name = "Sophia Moore", Email = "sophia.moore@example.com" },
            new() { Name = "William Jackson", Email = "william.jackson@example.com" },
            new() { Name = "Ava White", Email = "ava.white@example.com" },
            new() { Name = "Robert Harris", Email = "robert.harris@example.com" },
            new() { Name = "Isabella Martin", Email = "isabella.martin@example.com" },
            new() { Name = "Christopher Thompson", Email = "christopher.thompson@example.com" }
        };

        context.Customers.AddRange(customers);
        await context.SaveChangesAsync();

        var now = DateTime.UtcNow;

        var orders = new List<Order>
        {
            // John
            new()
            {
                CustomerId = customers[0].Id,
                ProductName = "Wireless Headphones",
                Amount = 149.99m,
                OrderDate = now.AddDays(-5),
                IsDamaged = true
            },
            new()
            {
                CustomerId = customers[0].Id,
                ProductName = "USB-C Charging Cable",
                Amount = 24.99m,
                OrderDate = now.AddDays(-10)
            },

            // Sarah
            new()
            {
                CustomerId = customers[1].Id,
                ProductName = "Laptop Pro 14",
                Amount = 1299.99m,
                OrderDate = now.AddDays(-8),
                IsDamaged = true
            },
            new()
            {
                CustomerId = customers[1].Id,
                ProductName = "Laptop Sleeve",
                Amount = 39.99m,
                OrderDate = now.AddDays(-3),
                IsFinalSale = true
            },

            // Michael
            new()
            {
                CustomerId = customers[2].Id,
                ProductName = "Smart Watch",
                Amount = 299.99m,
                OrderDate = now.AddDays(-12),
                IsIncorrectItem = true
            },
            new()
            {
                CustomerId = customers[2].Id,
                ProductName = "Sports Band",
                Amount = 49.99m,
                OrderDate = now.AddDays(-45)
            },

            // Emily
            new()
            {
                CustomerId = customers[3].Id,
                ProductName = "4K Monitor",
                Amount = 449.99m,
                OrderDate = now.AddDays(-7)
            },

            // Daniel
            new()
            {
                CustomerId = customers[4].Id,
                ProductName = "Gaming Laptop",
                Amount = 1599.99m,
                OrderDate = now.AddDays(-4)
            },
            new()
            {
                CustomerId = customers[4].Id,
                ProductName = "Gaming Mouse",
                Amount = 79.99m,
                OrderDate = now.AddDays(-15),
                IsDamaged = true
            },

            // Jessica
            new()
            {
                CustomerId = customers[5].Id,
                ProductName = "Coffee Machine",
                Amount = 199.99m,
                OrderDate = now.AddDays(-20),
                IsIncorrectItem = true
            },

            // David
            new()
            {
                CustomerId = customers[6].Id,
                ProductName = "Mechanical Keyboard",
                Amount = 129.99m,
                OrderDate = now.AddDays(-35)
            },

            // Olivia
            new()
            {
                CustomerId = customers[7].Id,
                ProductName = "Air Purifier",
                Amount = 349.99m,
                OrderDate = now.AddDays(-6),
                IsDamaged = true
            },

            // James
            new()
            {
                CustomerId = customers[8].Id,
                ProductName = "Office Chair",
                Amount = 599.99m,
                OrderDate = now.AddDays(-9)
            },

            // Sophia
            new()
            {
                CustomerId = customers[9].Id,
                ProductName = "Running Shoes",
                Amount = 119.99m,
                OrderDate = now.AddDays(-2),
                IsIncorrectItem = true
            },

            // William
            new()
            {
                CustomerId = customers[10].Id,
                ProductName = "Bluetooth Speaker",
                Amount = 89.99m,
                OrderDate = now.AddDays(-10),
                IsFinalSale = true
            },

            // Ava
            new()
            {
                CustomerId = customers[11].Id,
                ProductName = "Tablet",
                Amount = 699.99m,
                OrderDate = now.AddDays(-14),
                IsDamaged = true
            },

            // Robert
            new()
            {
                CustomerId = customers[12].Id,
                ProductName = "External SSD 2TB",
                Amount = 179.99m,
                OrderDate = now.AddDays(-4)
            },

            // Isabella
            new()
            {
                CustomerId = customers[13].Id,
                ProductName = "Digital Camera",
                Amount = 899.99m,
                OrderDate = now.AddDays(-50)
            },

            // Christopher
            new()
            {
                CustomerId = customers[14].Id,
                ProductName = "Smartphone",
                Amount = 799.99m,
                OrderDate = now.AddDays(-11),
                IsIncorrectItem = true
            }
        };

        context.Orders.AddRange(orders);
        await context.SaveChangesAsync();
    }
}
