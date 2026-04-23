using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    //public class OrderContextSeed
    //{
    //    public static async Task SeedAsync(
    //        OrderContext orderContext,
    //        ILogger<OrderContextSeed> logger
    //    )
    //    {
    //        if (!orderContext.Orders.Any())
    //        {
    //            orderContext.Orders.AddRange(GetPreconfiguredOrders());
    //            await orderContext.SaveChangesAsync();
    //            logger.LogInformation(
    //                "Seed database associated with context {DbContextName}",
    //                typeof(OrderContext).Name
    //            );
    //        }
    //    }

    //    private static IEnumerable<Order> GetPreconfiguredOrders()
    //    {
    //        return new List<Order>
    //        {
    //            new Order()
    //            {
    //                UserName = "swn",
    //                FirstName = "Mehmet",
    //                LastName = "Ozkaya",
    //                EmailAddress = "ezozkme@gmail.com",
    //                AddressLine = "Bahcelievler",
    //                Country = "Turkey",
    //                TotalPrice = 350,
    //            },
    //        };
    //    }
    //}
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            try
            {
                if (!context.Orders.Any())
                {
                    var orders = new List<Order>
                    {
                        new Order
                        {
                            UserName = "minhtran",
                            FirstName = "Minh",
                            LastName = "Tran",
                            EmailAddress = "minhtran92@gmail.com",
                            AddressLine = "Cau Giay",
                            Country = "Vietnam",
                            TotalPrice = 420,
                            State = "Hanoi",
                            ZipCode = "100000",
                            CardName = "Minh Tran",
                            CardNumber = "4532891122334455",
                            Expiration = "11/26",
                            CVV = "456",
                            PaymentMethod = 2,
                            CreatedBy = "admin",
                            CreatedDate = DateTime.Now,
                            LastModifiedBy = "admin",
                            LastModifiedDate = DateTime.Now,
                        },
                        new Order
                        {
                            UserName = "linhpham",
                            FirstName = "Linh",
                            LastName = "Pham",
                            EmailAddress = "linhpham88@gmail.com",
                            AddressLine = "District 1",
                            Country = "Vietnam",
                            TotalPrice = 275,
                            State = "Ho Chi Minh",
                            ZipCode = "700000",
                            CardName = "Linh Pham",
                            CardNumber = "5123456789012346",
                            Expiration = "08/27",
                            CVV = "789",
                            PaymentMethod = 1,
                            CreatedBy = "system",
                            CreatedDate = DateTime.Now,
                            LastModifiedBy = "system",
                            LastModifiedDate = DateTime.Now,
                        },
                        new Order
                        {
                            UserName = "johnsmith",
                            FirstName = "John",
                            LastName = "Smith",
                            EmailAddress = "johnsmith@gmail.com",
                            AddressLine = "5th Avenue",
                            Country = "USA",
                            TotalPrice = 999,
                            State = "New York",
                            ZipCode = "10001",
                            CardName = "John Smith",
                            CardNumber = "6011223344556677",
                            Expiration = "03/28",
                            CVV = "321",
                            PaymentMethod = 3,
                            CreatedBy = "api",
                            CreatedDate = DateTime.Now,
                            LastModifiedBy = "api",
                            LastModifiedDate = DateTime.Now,
                        },
                        new Order
                        {
                            UserName = "sakura",
                            FirstName = "Sakura",
                            LastName = "Tanaka",
                            EmailAddress = "sakura.tanaka@gmail.com",
                            AddressLine = "Shibuya",
                            Country = "Japan",
                            TotalPrice = 610,
                            State = "Tokyo",
                            ZipCode = "150-0002",
                            CardName = "Sakura Tanaka",
                            CardNumber = "3566002020360505",
                            Expiration = "07/29",
                            CVV = "654",
                            PaymentMethod = 2,
                            CreatedBy = "batch",
                            CreatedDate = DateTime.Now,
                            LastModifiedBy = "batch",
                            LastModifiedDate = DateTime.Now,
                        },
                        new Order
                        {
                            UserName = "alexdoe",
                            FirstName = "Alex",
                            LastName = "Doe",
                            EmailAddress = "alexdoe@gmail.com",
                            AddressLine = "Oxford Street",
                            Country = "UK",
                            TotalPrice = 150,
                            State = "London",
                            ZipCode = "W1D 1BS",
                            CardName = "Alex Doe",
                            CardNumber = "4000123412341234",
                            Expiration = "05/26",
                            CVV = "987",
                            PaymentMethod = 1,
                            CreatedBy = "web",
                            CreatedDate = DateTime.Now,
                            LastModifiedBy = "web",
                            LastModifiedDate = DateTime.Now,
                        },
                    };

                    await context.Orders.AddRangeAsync(orders);
                    await context.SaveChangesAsync();
                    logger.LogInformation(
                        "Seed database associated with context {DbContextName}",
                        typeof(OrderContext).Name
                    );
                }
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while seeding the database with message {Message}",
                    ex.Message
                );
            }
        }
    }
}
