namespace VerstaTask.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using VerstaTask.Entities;
    using VerstaTask.Interfaces;
    using VerstaTask.Models;
    using VerstaTask.Services;

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IErrorService _errorService;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _errorService = new ErrorService();
        }

        public async Task<ActionResult> List(int page = 1)
        {
            var orders = await _orderRepository.GetAllAsync();
            orders = orders.OrderBy(x => x.Id).ToList();
            return View(orders);
        }

        public async Task<ActionResult> Delete(long id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return View("Error", _errorService.GenerateError<Order>(id));

            await _orderRepository.DeleteAsync(id);
            return RedirectToAction("List");
        }

        public async Task<ActionResult> DeleteAll()
        {
            var order = _orderRepository.GetAllAsync().Result.Any();

            if (order == false)
                return View("Error", _errorService.GenerateError<Order>(1));

            await _orderRepository.DeleteAllAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var order = _orderRepository.GetByIdAsync(id).Result;

            if (order == null)
                return View("Error", _errorService.GenerateError<Order>(id));

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
                    return View("Error", _errorService.GenerateError<Order>(model.Id));

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
