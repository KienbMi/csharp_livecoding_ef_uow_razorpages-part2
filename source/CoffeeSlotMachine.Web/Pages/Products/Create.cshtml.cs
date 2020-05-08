using System.Threading.Tasks;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeSlotMachine.Web.Pages.Products
{
public class CreateModel : PageModel
{
  private readonly IUnitOfWork _unitOfWork;

  public CreateModel(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  [BindProperty]
  public Product Product { get; set; }

  public IActionResult OnGet()
  {
      return Page();
  }

  public async Task<IActionResult> OnPost()
  {
    if(!ModelState.IsValid)
    {
      return Page();
    }

    await _unitOfWork.Products.AddAsync(Product);
    await _unitOfWork.SaveAsync();

    return RedirectToPage("./Index");
  }
}
}