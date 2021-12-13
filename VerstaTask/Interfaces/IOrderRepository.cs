namespace VerstaTask.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VerstaTask.Entities;
    using VerstaTask.Models;

    public interface IOrderRepository
    {
        public Task<Order> GetByIdAsync(long id);

        public Task<List<Order>> GetAllAsync();

        public Task DeleteByIdAsync(long id);

        public Task EditAsync(OrderEditDto model);

        public Task AddAsync(OrderAddDto model);

        public Task DeleteAsync(long id);

        public Task DeleteAllAsync();
    }
}
