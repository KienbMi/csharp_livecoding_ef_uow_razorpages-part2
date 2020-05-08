using CoffeeSlotMachine.Core.DataTransferObjects;
using CoffeeSlotMachine.Core.Entities;
using System.Threading.Tasks;

namespace CoffeeSlotMachine.Core.Contracts
{
  public interface IProductRepository
  {
    Task<Product[]> GetAllAsync();
    Task<Product[]> GetAllWithOrders();
    Task<Product[]> GetByNameAsync(string filter);

    Task<Product> GetByIdAsync(int id);

    Task<ProductDto[]> GetProductDtosAsync();

    Task<Product> GetByTypeNameAsync(string coffeeTypeName);

    Task AddAsync(Product product);
    void Update(Product product);
    void Remove(Product product);
    
  }
}