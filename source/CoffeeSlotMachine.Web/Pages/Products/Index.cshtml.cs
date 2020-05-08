using System.Threading.Tasks;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeSlotMachine.Web.Pages.Products
{
  public class IndexModel : PageModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Product[] Products { get; set; }
    
    public string ProductFilter { get; set; }

    public async Task OnGet(string searchString)
    {
      if(!string.IsNullOrEmpty(searchString))
      {
        Products = await _unitOfWork.Products.GetByNameAsync(searchString);
        ProductFilter = searchString;
      } else
      {
        Products = await _unitOfWork.Products.GetAllAsync();
      }
      
    }
  }
}