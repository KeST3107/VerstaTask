namespace VerstaTask
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VerstaTask.EF;
    using VerstaTask.Interfaces;
    using VerstaTask.Models;

    public class OrderRepository : IOrderRepository
    {
        private readonly VerstaContext _context;

        public OrderRepository(VerstaContext context)
        {
            _context = context;
        }

        public Order GetById(long id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            return order;
        }

        public List<Order> GetAll()
        {
            var order = _context.Orders.ToList();
            return order;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var order = GetById(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(OrderEditDto model)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == model.Id);

            if (order != null)
            {
                order.SenderCity = model.SenderCity;
                order.SenderAddress = model.SenderAddress;
                order.RecipientCity = model.RecipientCity;
                order.RecipientAddress = model.RecipientAddress;
                order.CargoWeight = model.CargoWeight;
                order.PickupDate = model.PickupDate;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }

        }

        public async Task AddAsync(OrderAddDto model)
        {
            var order = new Order
            {
                Id = _context.Orders.Any() == false ? 1 : _context.Orders.Max(x=>x.Id) + 1,
                SenderCity = model.SenderCity,
                SenderAddress = model.SenderAddress,
                RecipientCity = model.RecipientCity,
                RecipientAddress = model.RecipientAddress,
                CargoWeight = model.CargoWeight,
                PickupDate = model.PickupDate
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync()
        {
            if (_context.Orders != null)
            {
                _context.Orders.RemoveRange(_context.Orders);
                await _context.SaveChangesAsync();
            }
        }
    }
}
