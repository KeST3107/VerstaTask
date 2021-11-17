namespace VerstaTask.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VerstaTask.Models;

    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VerstaContext>();
                context.Database.Migrate();

                var orders = new List<Order>
                {
                    new Order
                    {
                        Id = 1,
                        SenderCity = "Воркута",
                        SenderAddress = "Улица Катаева д. 39",
                        RecipientCity = "Котлас",
                        RecipientAddress = "Улица Ульянова д. 9",
                        CargoWeight = 3.58f,
                        PickupDate = DateTime.Now
                    },
                    new Order
                    {
                        Id = 2,
                        SenderCity = "Котлас",
                        SenderAddress = "Улица Ульянова д. 9",
                        RecipientCity = "Казань",
                        RecipientAddress = "Улица Ульянова д. 9",
                        CargoWeight = 10.6f,
                        PickupDate = new DateTime(2005, 10, 5)
                    },
                };

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(orders);
                }

                context.SaveChanges();
            }
        }
    }
}
