using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeSlotMachine.Core.Contracts
{
  public interface ICoinRepository
  {
    Task<Coin[]> GetAllAsync();
    Task<Coin[]> GetOrderedDescendingByValueAsync();
    Task<Coin> GetByIdAsync(int id);
    
    Task AddAsync(Coin coin);
    Task<bool> DeleteAsync(int id);
  }
}