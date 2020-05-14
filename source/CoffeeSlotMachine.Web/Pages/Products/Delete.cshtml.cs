using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeSlotMachine.Web.Pages.Products
{
  public class DeleteModel : PageModel
  {

    private readonly IUnitOfWork _unitOfWork;

    public DeleteModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [BindProperty]
    public Product Product { get; set; }

    public async Task<IActionResult> OnGet(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Product = await _unitOfWork.Products.GetByIdAsync(id.Value);
      if (Product == null)
      {
        return NotFound();
      }

      return Page();
    }

    public async Task<IActionResult> OnPost()
    {
      if (Product == null)
      {
        return NotFound();
      }

      Product = await _unitOfWork.Products.GetByIdAsync(Product.Id);
      if (Product == null)
      {
        return NotFound();
      }

      _unitOfWork.Products.Remove(Product);
      await _unitOfWork.SaveAsync();

      return RedirectToPage("./Index");
    }
  }
}