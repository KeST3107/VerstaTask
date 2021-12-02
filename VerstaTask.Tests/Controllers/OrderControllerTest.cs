namespace VerstaTask.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using VerstaTask.Controllers;
    using VerstaTask.Entities;
    using VerstaTask.Interfaces;
    using VerstaTask.Models;
    using VerstaTask.Services;
    using Xunit;

    public class OrderControllerTest
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrderController _controller;

        public OrderControllerTest()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _controller = new OrderController(_mockRepository.Object);
            _mockRepository.Setup(repo => repo.GetAllAsync().Result)
                .Returns(new List<Order>() {
                    new Order
                {
                    Id = 1,
                    SenderCity = "Воркута",
                    SenderAddress = "Улица Пушкина, дом 39, квартира 57",
                    RecipientCity = "Котлас",
                    RecipientAddress = "Улица Ипатова, дом 20, кв 220",
                    CargoWeight = 3.05f,
                    PickupDate = new DateTime(2021,11, 29)
                }, new Order
                {
                    Id = 2,
                    SenderCity = "Казань",
                    SenderAddress = "Улица Катаева, дом 11, квартира 15",
                    RecipientCity = "Москва",
                    RecipientAddress = "Улица Ленина, дом 14, кв 111",
                    CargoWeight = 25f,
                    PickupDate = new DateTime(2021,11, 25)
                } });
        }

        [Fact]
        public async void ListReturnsViewForm()
        {
            var result = await _controller.List();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ListReturnsNumberOfOrders()
        {
            var result = await _controller.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var orders = Assert.IsType<List<Order>>(viewResult.Model);
            Assert.Equal(2,orders.Count());
            Assert.Equal("Казань",orders[1].SenderCity);
            Assert.Equal("Улица Катаева, дом 11, квартира 15",orders[1].SenderAddress);
        }

        /*[Fact]
        public void DeleteOrder()
        {
            _mockRepository.Setup(p => p.DeleteAsync(1));
            var result = _controller.List();
            var viewResult = Assert.IsType<ViewResult>(result);
            var orders = Assert.IsType<List<Order>>(viewResult.Model);
            Assert.Equal(1,orders.Count());
            Assert.Equal("Казань",orders[0].SenderCity);
            Assert.Equal("Улица Катаева, дом 11, квартира 15",orders[0].SenderAddress);
        }*/

        /*[HttpGet]
        public ActionResult Add() => View();

        [HttpPost]
        public async Task<ActionResult> Add(OrderAddDto model)
        {
        }

        public void DeleteTest(long id)
        {

        }

        public async Task<ActionResult> DeleteAll()
        {
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OrderEditDto model)
        {
        }*/

    }
}
