using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSlotMachine.Web.Pages.Products
{
  public class EditModel : PageModel
  {

    private readonly IUnitOfWork _unitOfWork;

    public EditModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    [BindProperty]
    public Product Product { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnGet(int? id)
    {
      if (!id.HasValue)
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
      if (!ModelState.IsValid)
      {
        return Page();
      }

      Product productInDb = await _unitOfWork.Products.GetByIdAsync(Product.Id);
      if (productInDb == null)
      {
        return NotFound();
      }

      productInDb.Name = Product.Name;
      productInDb.PriceInCents = Product.PriceInCents;

      try
      {
        await _unitOfWork.SaveAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        // logging;
        ErrorMessage = "Datensatz wurde in der DB bereits verändert - bitte Daten erneut speichern!";
          return Page();
      }

      return RedirectToPage("./Index");

    }
  }
}