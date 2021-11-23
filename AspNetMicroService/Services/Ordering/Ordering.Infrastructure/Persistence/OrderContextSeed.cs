using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "amt", FirstName = "Amit", LastName = "Ingole", EmailAddress = "amit@gmail.com", AddressLine = "Jayprakash Nagar", Country = "India", TotalPrice = 350 },
                new Order() {UserName = "snv", FirstName = "Saanvi", LastName = "Ingole", EmailAddress = "saanvi@gmail.com", AddressLine = "Jayprakash Nagar", Country = "India", TotalPrice = 250 }
            };

        }
    }
}
