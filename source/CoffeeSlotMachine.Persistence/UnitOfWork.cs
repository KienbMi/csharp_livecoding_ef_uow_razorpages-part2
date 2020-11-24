using CoffeeSlotMachine.Core.Contracts;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoffeeSlotMachine.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CoffeeSlotMachine.Persistence
{
  public class UnitOfWork : IUnitOfWork, IDisposable
  {

    readonly ApplicationDbContext _dbContext;
    private bool _isDisposed;

    /// <summary>
    /// ConnectionString kommt von der Asp.Net Core App und ist
    /// dort in den app-Settings (JSON) gespeichert
    /// </summary>
    public UnitOfWork()
    {
      _dbContext = new ApplicationDbContext();
      Products = new ProductRepository(_dbContext);
      Orders = new OrderRepository(_dbContext);
      Coins = new CoinRepository(_dbContext);
    }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
      Products = new ProductRepository(_dbContext);
      Orders = new OrderRepository(_dbContext);
      Coins = new CoinRepository(_dbContext);
    }


    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public ICoinRepository Coins { get; }

    public async Task SaveAsync()
    {
      var entities = _dbContext.ChangeTracker.Entries()
          .Where(entity => entity.State == EntityState.Added
                           || entity.State == EntityState.Modified)
          .Select(e => e.Entity);
      foreach (var entity in entities)
      {
        await ValidateEntity(entity);
      }
      
      await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Validierungen auf DbContext-Ebene
    /// </summary>
    /// <param name="entity"></param>
    private async Task ValidateEntity(object entity)
    {
      if (entity is Product product)
      {
        if (await _dbContext.Products.AnyAsync(p => p.Id != product.Id && p.Name == product.Name))
        {
          throw new ValidationException($"Produkt mit Namen {product.Name} existiert bereits.");
        }
      }
    }

    public async Task InitializeDatabaseAsync()
    {
      await _dbContext.Database.EnsureDeletedAsync();
      await _dbContext.Database.MigrateAsync();
    }

    public async Task DeleteDatabaseAsync()
    {
      await _dbContext.Database.EnsureDeletedAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        if (disposing)
        {
          _dbContext.Dispose();
        }
      }
      _isDisposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
