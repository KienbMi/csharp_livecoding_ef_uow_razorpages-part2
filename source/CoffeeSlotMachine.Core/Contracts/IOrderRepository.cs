using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeSlotMachine.Core.Contracts
{
    public interface IOrderRepository
    {
    Task<Order[]> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task<Order[]> GetAllWithProductAsync();
    Task<Order> GetByIdWithProductAndCoinsAsync(int id);

    Task InsertAsync(Order order);
    Task<bool> DeleteAsync(int id);
  }
}