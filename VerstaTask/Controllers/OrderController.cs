namespace VerstaTask.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using VerstaTask.Interfaces;
    using VerstaTask.Models;

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public ActionResult List(int page = 1)
        {
            var orders = _orderRepository.GetAllAsync().Result.OrderBy(x => x.Id);
            return View(orders);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;

            if (order == null)
                return View("Error", new ErrorViewModel
                {
                    EntityName = nameof(Order),
                    EntityId = id.ToString(),
                    ErrorMessage = $"Не найдена сущность {nameof(Order)} с id = {id}."
                });

            await _orderRepository.DeleteAsync(id);
            return RedirectToAction("List");
        }

        public async Task<ActionResult> DeleteAll()
        {
            var order = _orderRepository.GetAllAsync().Result.Any();

            if (order == false)
                return View("Error", new ErrorViewModel
                {
                    EntityName = nameof(Order),
                    EntityId = "1",
                    ErrorMessage = $"Не найдены существующие заказы!"
                });

            await _orderRepository.DeleteAllAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;

            if (order == null)
                return View("Error", new ErrorViewModel
                {
                    EntityName = nameof(Order),
                    EntityId = id.ToString(),
                    ErrorMessage = $"Не найдена сущность {nameof(Order)} с id = {id}."
                });

            var orderModel = new OrderEditDto()
            {
                SenderCity = order.SenderCity,
                SenderAddress = order.SenderAddress,
                RecipientCity = order.RecipientCity,
                RecipientAddress = order.RecipientAddress,
                CargoWeight = order.CargoWeight,
                PickupDate = order.PickupDate
            };

            return View(orderModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OrderEditDto model)
        {
            if (ModelState.IsValid)
            {
                var order = _orderRepository.GetByIdAsync(model.Id).Result;

                if (order == null)
                    return View("Error", new ErrorViewModel
                    {
                        EntityName = nameof(Order),
                        EntityId = model.Id.ToString(),
                        ErrorMessage = $"Не найдена сущность {nameof(Order)} с id = {model.Id}."
                    });

                await _orderRepository.EditAsync(model);

                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Add() => View();

        [HttpPost]
        public async Task<ActionResult> Add(OrderAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.AddAsync(model);

                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
