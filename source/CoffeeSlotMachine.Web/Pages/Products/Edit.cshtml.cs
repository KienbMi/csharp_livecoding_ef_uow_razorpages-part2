using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
      if (!ModelState.IsValid)
      {
        return Page();
      }

      Product dbProduct = await _unitOfWork.Products.GetByIdAsync(Product.Id);
      dbProduct.Name = Product.Name;
      dbProduct.PriceInCents = Product.PriceInCents;

      _unitOfWork.Products.Update(dbProduct);

      try
      {
        await _unitOfWork.SaveAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if(await _unitOfWork.Products.GetByIdAsync(Product.Id) == null)
        {
          return NotFound();
        }

        throw;
      }

      return RedirectToPage("./Index");
    }
  }
}