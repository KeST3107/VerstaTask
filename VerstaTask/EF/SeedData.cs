namespace VerstaTask.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VerstaTask.Entities;
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
                        PickupDate = new DateTime(2021,11,15,14,33,00)
                    },
                    new Order
                    {
                        Id = 2,
                        SenderCity = "Котлас",
                        SenderAddress = "Улица Ульянова д. 9",
                        RecipientCity = "Казань",
                        RecipientAddress = "Улица Ульянова д. 9",
                        CargoWeight = 10.6f,
                        PickupDate = new DateTime(2021, 10, 5,18,55,00)
                    },
                    new Order
                    {
                        Id = 3,
                        SenderCity = "Талдом",
                        SenderAddress = "бульвар Ленина, 38",
                        RecipientCity = "Ногинск",
                        RecipientAddress = "бульвар Домодедовская, 29",
                        CargoWeight = 35.58f,
                        PickupDate = new DateTime(2020,11,15,09,15,00)
                    },
                    new Order
                    {
                        Id = 4,
                        SenderCity = "Серебряные Пруды",
                        SenderAddress = "спуск Славы, 93",
                        RecipientCity = "Ногинск",
                        RecipientAddress = "шоссе Гагарина, 84",
                        CargoWeight = 18f,
                        PickupDate = new DateTime(2005, 10, 5,08,15,00)
                    },
                    new Order
                    {
                        Id = 5,
                        SenderCity = "Кашира",
                        SenderAddress = "ул. Сталина, 28",
                        RecipientCity = "Коломна",
                        RecipientAddress = "въезд Ломоносова, 87",
                        CargoWeight = 39f,
                        PickupDate = new DateTime(2021,05,15,14,33,00)
                    },
                    new Order
                    {
                        Id = 6,
                        SenderCity = "Подольск",
                        SenderAddress = "въезд Бухарестская, 60",
                        RecipientCity = "Серебряные Пруды",
                        RecipientAddress = "спуск Чехова, 55",
                        CargoWeight = 199f,
                        PickupDate = new DateTime(2015, 10, 25,15,00,00)
                    },
                    new Order
                    {
                        Id = 7,
                        SenderCity = "Луховицы",
                        SenderAddress = "пр. Ладыгина, 07",
                        RecipientCity = "Одинцово",
                        RecipientAddress = "бульвар Ленина, 37",
                        CargoWeight = 32f,
                        PickupDate = new DateTime(2021,5,15,17,8,00)
                    },
                    new Order
                    {
                        Id = 8,
                        SenderCity = "Луховицы",
                        SenderAddress = "проезд Космонавтов, 91",
                        RecipientCity = "Лотошино",
                        RecipientAddress = "пл. 1905 года, 64",
                        CargoWeight = 10.68f,
                        PickupDate = new DateTime(2010, 10, 5,13,44,00)
                    },
                    new Order
                    {
                        Id = 9,
                        SenderCity = "Озёры",
                        SenderAddress = "пл. Косиора, 72",
                        RecipientCity = "Воскресенск",
                        RecipientAddress = "пр. Сталина, 05",
                        CargoWeight = 39f,
                        PickupDate = new DateTime(2021,9,15,12,50,00)
                    },
                    new Order
                    {
                        Id = 10,
                        SenderCity = "Дорохово",
                        SenderAddress = "пер. Гагарина, 52",
                        RecipientCity = "Озёры",
                        RecipientAddress = "пр. Ленина, 24",
                        CargoWeight = 0.55f,
                        PickupDate = new DateTime(2020, 6, 15,12,15,00)
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
