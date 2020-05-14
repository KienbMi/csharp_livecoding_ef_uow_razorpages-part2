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
  public class DetailsModel : PageModel
  {

    private readonly IUnitOfWork _unitOfWork;

    public DetailsModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Product Product { get; set; }
    
    public async Task<IActionResult> OnGet(int? id)
    {
      if(!id.HasValue)
      {
        return NotFound();
      }

      Product = await _unitOfWork.Products.GetByIdAsync(id.Value);
      if(Product == null)
      {
        return NotFound();
      }

      return Page();
    }
  }
}