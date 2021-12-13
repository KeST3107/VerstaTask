namespace VerstaTask.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using VerstaTask.Controllers;
    using VerstaTask.EF;
    using VerstaTask.Entities;
    using VerstaTask.Models;
    using VerstaTask.Repositories;
    using Xunit;

    public abstract class OrderControllerTest
    {
        protected DbContextOptions<VerstaContext> ContextOptions { get; }

        protected OrderControllerTest(DbContextOptions<VerstaContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Order
                {
                    Id = 1,
                    SenderCity = "Воркута",
                    SenderAddress = "Улица Пушкина, дом 39, квартира 57",
                    RecipientCity = "Котлас",
                    RecipientAddress = "Улица Ипатова, дом 20, кв 220",
                    CargoWeight = 3.05f,
                    PickupDate = new DateTime(2021, 11, 29)
                };

                var two = new Order
                {
                    Id = 2,
                    SenderCity = "Казань",
                    SenderAddress = "Улица Катаева, дом 11, квартира 15",
                    RecipientCity = "Москва",
                    RecipientAddress = "Улица Ленина, дом 14, кв 111",
                    CargoWeight = 25f,
                    PickupDate = new DateTime(2021, 11, 25)
                };

                context.AddRange(one, two);

                context.SaveChanges();
            }
        }

        [Fact]
        public async void ListOrders()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);

                var result = await controller.List();
                var viewResult = Assert.IsType<ViewResult>(result);
                var orders = Assert.IsType<List<Order>>(viewResult.Model);
                Assert.Equal(2, orders.Count);
                Assert.Equal("Воркута", orders[0].SenderCity);
                Assert.Equal("Котлас", orders[0].RecipientCity);
                Assert.Equal(3.05f, orders[0].CargoWeight);
                Assert.Equal("Казань", orders[1].SenderCity);
                Assert.Equal("Москва", orders[1].RecipientCity);
                Assert.Equal(25f, orders[1].CargoWeight);
            }
        }

        [Fact]
        public async void DeleteItemById()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);
                await controller.Delete(1);
                var result = await controller.List();
                var viewResult = Assert.IsType<ViewResult>(result);
                var orders = Assert.IsType<List<Order>>(viewResult.Model);
                Assert.Single(orders);
                Assert.Equal("Казань", orders[0].SenderCity);
                Assert.Equal("Москва", orders[0].RecipientCity);
                Assert.Equal(25f, orders[0].CargoWeight);
            }
        }

        [Fact]
        public async void DeleteAll()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);
                await controller.DeleteAll();
                var result = await controller.List();
                var viewResult = Assert.IsType<ViewResult>(result);
                var orders = Assert.IsType<List<Order>>(viewResult.Model);
                Assert.Empty(orders);
            }
        }

        [Fact]
        public void EditGet()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);

                var result = controller.Edit(1);
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsType<OrderEditDto>(viewResult.Model);
                Assert.Equal(1, model.Id);
                Assert.Equal("Воркута", model.SenderCity);
                Assert.Equal("Улица Пушкина, дом 39, квартира 57", model.SenderAddress);
                Assert.Equal("Котлас", model.RecipientCity);
                Assert.Equal("Улица Ипатова, дом 20, кв 220", model.RecipientAddress);
                Assert.Equal(3.05f, model.CargoWeight);
                Assert.Equal(new DateTime(2021, 11, 29), model.PickupDate);
            }
        }

        [Fact]
        public async void EditPost()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);
                var model = new OrderEditDto
                {
                    Id = 2,
                    SenderCity = "Казань город",
                    SenderAddress = "Катаева, дом 11, квартира 15",
                    RecipientCity = "Москва город",
                    RecipientAddress = "Ленина, дом 14, кв 111",
                    CargoWeight = 1525f,
                    PickupDate = new DateTime(2021, 11, 25)
                };
                await controller.Edit(model);
                var result = await controller.List();
                var viewResult = Assert.IsType<ViewResult>(result);
                var orders = Assert.IsType<List<Order>>(viewResult.Model);
                Assert.Equal(2, orders.Count());
                Assert.Equal(2, orders[1].Id);
                Assert.Equal("Казань город", orders[1].SenderCity);
                Assert.Equal("Катаева, дом 11, квартира 15", orders[1].SenderAddress);
                Assert.Equal("Москва город", orders[1].RecipientCity);
                Assert.Equal("Ленина, дом 14, кв 111", orders[1].RecipientAddress);
                Assert.Equal(1525f, orders[1].CargoWeight);
                Assert.Equal(new DateTime(2021, 11, 25), orders[1].PickupDate);
            }
        }

        [Fact]
        public async void Add()
        {
            using (var context = new VerstaContext(ContextOptions))
            {
                var repository = new OrderRepository(context);
                var controller = new OrderController(repository);
                var model = new OrderAddDto
                {
                    SenderCity = "Казань город",
                    SenderAddress = "Катаева, дом 11, квартира 15",
                    RecipientCity = "Москва город",
                    RecipientAddress = "Ленина, дом 14, кв 111",
                    CargoWeight = 1525f,
                    PickupDate = new DateTime(2021, 11, 25)
                };

                await controller.Add(model);
                var result = await controller.List();
                var viewResult = Assert.IsType<ViewResult>(result);
                var orders = Assert.IsType<List<Order>>(viewResult.Model);
                Assert.Equal(3, orders.Count());
                Assert.Equal(3, orders[2].Id);
                Assert.Equal("Казань город", orders[2].SenderCity);
                Assert.Equal("Катаева, дом 11, квартира 15", orders[2].SenderAddress);
                Assert.Equal("Москва город", orders[2].RecipientCity);
                Assert.Equal("Ленина, дом 14, кв 111", orders[2].RecipientAddress);
                Assert.Equal(1525f, orders[2].CargoWeight);
                Assert.Equal(new DateTime(2021, 11, 25), orders[2].PickupDate);
            }
        }
    }
}
