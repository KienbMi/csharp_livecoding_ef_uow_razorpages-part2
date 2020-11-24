using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
      if (!ModelState.IsValid)
      {
        return Page();
      }


      await _unitOfWork.Products.AddAsync(Product);

      try
      {
        await _unitOfWork.SaveAsync();
      }
      catch (ValidationException validationEx)
      {
        ModelState.AddModelError("Product.Name", validationEx.Message);
        return Page();
      }

      return RedirectToPage("./Index");
    }
  }
}