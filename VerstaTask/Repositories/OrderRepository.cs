namespace VerstaTask.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VerstaTask.EF;
    using VerstaTask.Entities;
    using VerstaTask.Interfaces;
    using VerstaTask.Models;

    public class OrderRepository : IOrderRepository
    {
        private readonly VerstaContext _context;

        public OrderRepository(VerstaContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(long id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var order = await GetByIdAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(OrderEditDto model)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == model.Id);

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
                Id = _context.Orders.Any() == false ? 1 : _context.Orders.Max(x => x.Id) + 1,
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
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
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
