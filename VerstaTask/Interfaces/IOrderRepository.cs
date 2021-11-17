namespace VerstaTask.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VerstaTask.Models;

    public interface IOrderRepository
    {
        public Order GetById(long id);

        public List<Order> GetAll();

        public Task DeleteByIdAsync(long id);

        public Task EditAsync(OrderEditDto model);

        public Task AddAsync(OrderEditDto model);

        public Task DeleteAsync(long id);

        public Task DeleteAllAsync();
    }
}
